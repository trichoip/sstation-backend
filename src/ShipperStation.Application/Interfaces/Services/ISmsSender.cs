﻿namespace ShipperStation.Application.Interfaces.Services;
public interface ISmsSender
{
    Task SendAsync(string phoneNumber, string message, CancellationToken cancellationToken = default);
}
