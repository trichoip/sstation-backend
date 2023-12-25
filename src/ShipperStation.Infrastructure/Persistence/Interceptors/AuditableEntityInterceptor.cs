using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ShipperStation.Application.Interfaces.Services;
using ShipperStation.Domain.Common.Interfaces;
using ShipperStation.Domain.Entities;
using ShipperStation.Domain.Enums;

namespace ShipperStation.Infrastructure.Persistence.Interceptors;

public class AuditableEntityInterceptor(ICurrentUserService currentUserService) : SaveChangesInterceptor
{
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        await UpdateEntities(eventData.Context);
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public async Task UpdateEntities(DbContext? context)
    {
        if (context == null) return;

        foreach (var entry in context.ChangeTracker.Entries())
        {
            // Save audit log table
            var auditEntry = GetAuditEntry(entry);
            if (auditEntry != null)
            {
                var audit = auditEntry.ToAudit();

                audit.CreatedBy = currentUserService.CurrentUserId;
                audit.ModifiedBy = currentUserService.CurrentUserId;
                audit.UserId = currentUserService.CurrentUserId;
                await context.AddAsync(audit);
            }
        }

        foreach (var entry in context.ChangeTracker.Entries<IAuditableEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedBy = currentUserService.CurrentUserId;
                entry.Entity.CreatedAt = DateTimeOffset.UtcNow;
            }

            if (entry.State == EntityState.Added || entry.State == EntityState.Modified || entry.HasChangedOwnedEntities())
            {
                entry.Entity.ModifiedBy = currentUserService.CurrentUserId;
                entry.Entity.ModifiedAt = DateTimeOffset.UtcNow;
            }

            if (entry.State == EntityState.Deleted)
            {
                entry.State = EntityState.Modified;
                entry.Entity.DeletedBy = currentUserService.CurrentUserId;
                entry.Entity.DeletedAt = DateTimeOffset.UtcNow;
            }
        }
    }

    private AuditEntry? GetAuditEntry(EntityEntry entry)
    {
        if (entry.Entity is Audit ||
            entry.State == EntityState.Detached ||
            entry.State == EntityState.Unchanged)
        {
            return null;
        }

        var auditEntry = new AuditEntry(entry);
        auditEntry.TableName = entry.Entity.GetType().Name;

        foreach (var property in entry.Properties)
        {
            var propertyName = property.Metadata.Name;
            if (property.Metadata.IsPrimaryKey())
            {
                auditEntry.KeyValues[propertyName] = property.CurrentValue ?? string.Empty;
                continue;
            }
            switch (entry.State)
            {
                case EntityState.Added:
                    auditEntry.AuditType = AuditType.Create;
                    auditEntry.NewValues[propertyName] = property.CurrentValue ?? string.Empty;
                    break;

                case EntityState.Deleted:
                    auditEntry.AuditType = AuditType.Delete;
                    auditEntry.OldValues[propertyName] = property.OriginalValue ?? string.Empty;
                    break;

                case EntityState.Modified:
                    if (property.IsModified)
                    {
                        auditEntry.ChangedColumns.Add(propertyName);
                        auditEntry.AuditType = AuditType.Update;
                        auditEntry.OldValues[propertyName] = property.OriginalValue ?? string.Empty;
                        auditEntry.NewValues[propertyName] = property.CurrentValue ?? string.Empty;
                    }
                    break;
            }
        }

        return auditEntry;
    }
}

public static class Extensions
{
    public static bool HasChangedOwnedEntities(this EntityEntry entry) =>
        entry.References.Any(r =>
            r.TargetEntry != null &&
            r.TargetEntry.Metadata.IsOwned() &&
            (r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified));
}
