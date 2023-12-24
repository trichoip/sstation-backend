﻿using Mapster;
using Microsoft.EntityFrameworkCore;
using ShipperStation.Application.Helpers;
using ShipperStation.Application.Interfaces.Repositories;
using ShipperStation.Infrastructure.Persistence.Data;
using System.Linq.Expressions;

namespace ShipperStation.Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly ApplicationDbContext context;
    private readonly DbSet<T> dbSet;

    public GenericRepository(ApplicationDbContext _context)
    {
        context = _context;
        dbSet = context.Set<T>();
    }

    public virtual async Task CreateAsync(
        T entity,
        CancellationToken cancellationToken = default)
    {
        await dbSet.AddAsync(entity, cancellationToken);
    }

    public virtual async Task CreateRangeAsync(
        IEnumerable<T> entities,
        CancellationToken cancellationToken = default)
    {
        await dbSet.AddRangeAsync(entities, cancellationToken);
    }

    public virtual async Task<T?> FindByIdAsync(
        object id,
        CancellationToken cancellationToken = default)
    {
        return await dbSet.FindAsync(id, cancellationToken);
    }

    public async Task<T?> FindByAsync(
        Expression<Func<T, bool>> expression,
        Func<IQueryable<T>, IQueryable<T>>? includeFunc = null,
        CancellationToken cancellationToken = default)
    {
        IQueryable<T> query = dbSet;

        if (includeFunc != null)
        {
            query = includeFunc(query);
        }

        return await query.FirstOrDefaultAsync(expression, cancellationToken);
    }

    public virtual Task DeleteAsync(T entity)
    {
        dbSet.Remove(entity);
        return Task.CompletedTask;
    }

    public virtual Task DeleteRangeAsync(IEnumerable<T> entities)
    {
        dbSet.RemoveRange(entities);
        return Task.CompletedTask;
    }

    public virtual Task UpdateAsync(T entity)
    {
        dbSet.Update(entity);
        return Task.CompletedTask;
    }

    public async Task<bool> ExistsByAsync(
        Expression<Func<T, bool>>? expression = null,
        CancellationToken cancellationToken = default)
    {
        IQueryable<T> query = dbSet;

        if (expression != null)
        {
            query = query.Where(expression);
        }
        return await query.AnyAsync(cancellationToken);
    }

    public virtual async Task<TDTO?> FindByAsync<TDTO>(
        Expression<Func<T, bool>> expression,
        CancellationToken cancellationToken = default) where TDTO : class
    {
        IQueryable<T> query = dbSet;

        query = query.Where(expression);

        return await query.ProjectToType<TDTO>()
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IList<TDTO>> FindAsync<TDTO>(
        Expression<Func<T, bool>>? expression = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        CancellationToken cancellationToken = default) where TDTO : class
    {
        IQueryable<T> query = dbSet;

        if (expression != null)
        {
            query = query.Where(expression);
        }

        if (orderBy != null)
        {
            query = orderBy(query);
        }

        return await query.ProjectToType<TDTO>()
            .ToListAsync(cancellationToken);
    }

    public async Task<PaginatedList<TDTO>> FindAsync<TDTO>(
        int pageIndex = 0,
        int pageSize = 0,
        Expression<Func<T, bool>>? expression = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        CancellationToken cancellationToken = default) where TDTO : class
    {
        IQueryable<T> query = dbSet;

        if (expression != null)
        {
            query = query.Where(expression);
        }

        if (orderBy != null)
        {
            query = orderBy(query);
        }

        return await query.ProjectToType<TDTO>()
            .PaginatedListAsync(pageIndex, pageSize, cancellationToken);
    }

}