﻿namespace ShipperStation.Application.Models;
public record class BaseEntityResponse<TKey> where TKey : IEquatable<TKey>
{
    public TKey Id { get; set; } = default!;
}
