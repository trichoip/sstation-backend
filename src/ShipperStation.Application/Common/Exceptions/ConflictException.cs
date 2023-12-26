using ShipperStation.Application.Common.Resources;
using ShipperStation.Shared.Extensions;

namespace ShipperStation.Application.Common.Exceptions;
public class ConflictException : Exception
{
    public ConflictException() : base(Resource.Conflict) { }

    public ConflictException(string message) : base(message) { }

    public ConflictException(string message, Exception innerException) : base(message, innerException) { }

    public ConflictException(string name, object key) : base(Resource.EntityConflict.FormatWith(name, key)) { }
}
