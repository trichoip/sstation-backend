using ShipperStation.Shared.Helpers;
using System.Linq.Expressions;

namespace ShipperStation.Application.Interfaces.Repositories
{

    public interface IGenericRepository<T> where T : class
    {
        Task<bool> ExistsByAsync(
            Expression<Func<T, bool>>? expression = null,
            CancellationToken cancellationToken = default);

        Task<T?> FindByIdAsync(object id, CancellationToken cancellationToken = default);

        Task<T?> FindByAsync(
            Expression<Func<T, bool>> expression,
            Func<IQueryable<T>, IQueryable<T>>? includeFunc = null,
            CancellationToken cancellationToken = default);

        Task<IList<T>> FindAsync(
            bool IsTracking = false,
            Expression<Func<T, bool>>? expression = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            CancellationToken cancellationToken = default);

        Task<TDTO?> FindByAsync<TDTO>(
            Expression<Func<T, bool>> expression,
            CancellationToken cancellationToken = default) where TDTO : class;

        Task<IList<TDTO>> FindAsync<TDTO>(
            Expression<Func<T, bool>>? expression = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            CancellationToken cancellationToken = default) where TDTO : class;

        Task<PaginatedList<TDTO>> FindAsync<TDTO>(
            int pageIndex = 0,
            int pageSize = 0,
            Expression<Func<T, bool>>? expression = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            CancellationToken cancellationToken = default) where TDTO : class;

        Task UpdateAsync(T entity);
        Task CreateAsync(T entity, CancellationToken cancellationToken = default);
        Task CreateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
        Task DeleteAsync(T entity);
        Task DeleteRangeAsync(IEnumerable<T> entities);
    }
}
