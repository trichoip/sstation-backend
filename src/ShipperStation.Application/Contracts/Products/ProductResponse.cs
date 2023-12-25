namespace ShipperStation.Application.Contracts.Products;

public sealed record ProductResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Detail { get; set; } = default!;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}
