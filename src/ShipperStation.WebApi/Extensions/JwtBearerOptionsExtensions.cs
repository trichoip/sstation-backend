using Microsoft.AspNetCore.Authentication.JwtBearer;
using ShipperStation.Application.Common.Exceptions;

namespace ShipperStation.WebApi.Extensions;

public static class JwtBearerOptionsExtensions
{
    public static void HandleEvents(this JwtBearerOptions options)
    {
        options.Events = new JwtBearerEvents
        {
            OnForbidden = context =>
            {
                throw new ForbiddenAccessException();
            },

            OnChallenge = context =>
            {
                throw new UnauthorizedAccessException("You are not authorized to access this resource");
            },

            OnMessageReceived = context =>
            {
                var accessToken = context.Request.Query["access_token"];
                if (!string.IsNullOrEmpty(accessToken))
                {
                    context.Token = accessToken;
                }
                return Task.CompletedTask;
            }
        };
    }
}