using ShipperStation.Application.Common.Resources;

namespace ShipperStation.Application.Common.Exceptions;

public class ForbiddenAccessException : Exception
{
    public ForbiddenAccessException() : base(Resource.Forbidden) { }

    public ForbiddenAccessException(string message) : base(message) { }
}
