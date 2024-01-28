using ShipperStation.Shared.Pages;

namespace ShipperStation.Application.Features.Notifications.Models;
public sealed class NotificationPaginatedResponse : PaginatedResponse<NotificationResponse>
{
    public int CountUnread { get; init; }

    public NotificationPaginatedResponse(
        PaginatedList<NotificationResponse> paginatedList,
        int countUnread) : base(paginatedList)
    {
        CountUnread = countUnread;
    }

}
