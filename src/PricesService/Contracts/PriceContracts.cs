namespace PricesService.Contracts;

public sealed record PriceResponse(
    string Id,
    string ProductId,
    string StoreId,
    string PriceText,
    string? SaleText,
    string? QuantityText,
    string? UnitPriceText,
    DateTime? SaleDate,
    DateTime UpdatedAt);

public sealed record CreatePriceRequest(
    string ProductId,
    string StoreId,
    string PriceText,
    string? SaleText,
    string? QuantityText,
    string? UnitPriceText,
    DateTime? SaleDate);

public sealed record UpdatePriceRequest(
    string PriceText,
    string? SaleText,
    string? QuantityText,
    string? UnitPriceText,
    DateTime? SaleDate);
