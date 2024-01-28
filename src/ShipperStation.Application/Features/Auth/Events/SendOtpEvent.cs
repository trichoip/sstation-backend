using ShipperStation.Domain.Common;

namespace ShipperStation.Application.Features.Auth.Events;
internal sealed record SendOtpEvent(string PhoneNumber, string Otp) : BaseEvent;
