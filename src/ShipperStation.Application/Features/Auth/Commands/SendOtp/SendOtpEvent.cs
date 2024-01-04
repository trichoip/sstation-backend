using ShipperStation.Domain.Common;

namespace ShipperStation.Application.Features.Auth.Commands.SendOtp;
internal sealed record SendOtpEvent(string PhoneNumber, string Otp) : BaseEvent;
