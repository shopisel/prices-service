namespace PricesService.Contracts;

public sealed record PriceResponse(
    string Id,
    string ProductId,
    string StoreId,
    decimal Price,
    decimal? Sale,
    DateTime? SaleDate,
    DateTime UpdatedAt);

public sealed record CreatePriceRequest(
    string ProductId,
    string StoreId,
    decimal Price,
    decimal? Sale,
    DateTime? SaleDate);

public sealed record UpdatePriceRequest(
    decimal Price,
    decimal? Sale,
    DateTime? SaleDate);
