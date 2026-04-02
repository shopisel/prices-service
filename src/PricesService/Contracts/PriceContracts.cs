namespace PricesService.Contracts;

public sealed record PriceResponse(
    string Id,
    string ProductId,
    string StoreId,
    decimal Price,
    DateTime UpdatedAt);

public sealed record CreatePriceRequest(
    string ProductId,
    string StoreId,
    decimal Price);

public sealed record UpdatePriceRequest(
    decimal Price);
