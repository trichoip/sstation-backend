using LinqKit;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using ShipperStation.Application.Features.Stations.Models;
using ShipperStation.Application.Models.Pages;
using ShipperStation.Domain.Entities;
using ShipperStation.Shared.Pages;
using System.Linq.Expressions;
using System.Text.Json.Serialization;

namespace ShipperStation.Application.Features.Stations.Queries;
public sealed record GetStationsByStoreManagerQuery : PaginationRequest<Station>, IRequest<PaginatedResponse<StationResponse>>
{
    /// <summary>
    /// Search field is search for name or description or contact phone or address
    /// </summary>
    public string? Search { get; set; }

    [JsonIgnore] // for body
    [BindNever] // for query
    public Guid UserId { get; set; }

    public override Expression<Func<Station, bool>> GetExpressions()
    {
        if (!string.IsNullOrWhiteSpace(Search))
        {
            Search = Search.Trim();
            Expression = Expression
                // cái đầu tiên phải là And vì khởi tạo PredicateBuilder là true nên nếu là Or thì sẽ không có điều kiện nào được thêm vào
                // và đặc biệt là cái đầu tiên không có tính vào toán tử and hay or nó chỉ bắt đàu từ cái thứ 2, ví dụ: (name like %%) or (description like %%)
                // chứ không có and ở đầu nên cái đàu là And hay Or thì không có ý nghĩa gì cả mà chỉ có ý nghĩa khi khởi tạo PredicateBuilder ban đầu là gì
                // ví dụ: PredicateBuilder.New<Station>(false) thì cái đầu tiên là Or
                // còn PredicateBuilder.New<Station>(true) thì cái đầu tiên là And
                .And(sta => EF.Functions.Like(sta.Name, $"%{Search}%"))
                .Or(sta => EF.Functions.Like(sta.Description, $"%{Search}%"))
                .Or(sta => EF.Functions.Like(sta.ContactPhone, $"%{Search}%"))
                .Or(sta => EF.Functions.Like(sta.Address, $"%{Search}%"));
        }

        Expression = Expression.And(sta => sta.UserStations.Any(_ => _.UserId == UserId));

        // ((((Name like %%) or (Description like %%) or (ContactPhone like %%))) or (Address like %%)) and (UserStations exist)
        return Expression;
    }
}
