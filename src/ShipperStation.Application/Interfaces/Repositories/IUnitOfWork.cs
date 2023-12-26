namespace ShipperStation.Application.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<T> Repository<T>() where T : class;
        Task CommitAsync(CancellationToken cancellationToken = default);
        Task RollbackAsync();
    }
}
