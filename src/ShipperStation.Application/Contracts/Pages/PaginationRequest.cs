using LinqKit;
using ShipperStation.Application.Common.Constants;
using ShipperStation.Application.Common.Enums;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace ShipperStation.Application.Contracts.Pages;

public abstract class PaginationRequest<T> where T : class
{
    private const string Id = nameof(Id);
    private int _pageNumber = PaginationConstants.DefaultPageNumber;
    private int _pageSize = PaginationConstants.DefaultPageSize;

    public int PageNumber
    {
        get => _pageNumber;
        set => _pageNumber = value > 0
            ? value
            : PaginationConstants.DefaultPageNumber;
    }

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > 0 && value <= PaginationConstants.MaxPageSize
            ? value
            : PaginationConstants.DefaultPageSize;
    }

    public string? SortColumn { get; set; } = Id;

    public SortDirection? SortDir { get; set; } = SortDirection.Asc;

    protected Expression<Func<T, bool>> Expression = PredicateBuilder.New<T>(true);

    public abstract Expression<Func<T, bool>> GetExpressions();

    public Func<IQueryable<T>, IOrderedQueryable<T>>? GetOrder()
    {
        if (string.IsNullOrWhiteSpace(SortColumn)) return null;

        return query => query.OrderBy($"{SortColumn} {SortDir.ToString().ToLower()}");
    }

    public string? GetDynamicOrder()
    {
        if (string.IsNullOrWhiteSpace(SortColumn)) return null;

        return $"{SortColumn} {SortDir.ToString().ToLower()}";
    }

}