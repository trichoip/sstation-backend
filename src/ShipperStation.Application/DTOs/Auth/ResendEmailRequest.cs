﻿namespace ShipperStation.Application.DTOs.Auth;
public sealed class ResendEmailRequest
{
    public string Email { get; init; } = default!;
}
