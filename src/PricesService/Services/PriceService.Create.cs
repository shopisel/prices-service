using PricesService.Contracts;
using PricesService.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace PricesService.Services;

public partial class PriceService
{
    public async Task<PriceResponse> CreateAsync(
        CreatePriceRequest request,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(request.ProductId))
        {
            throw new ArgumentException("The productId is required.", nameof(request));
        }

        if (string.IsNullOrWhiteSpace(request.StoreId))
        {
            throw new ArgumentException("The storeId is required.", nameof(request));
        }

        if (request.Price <= 0)
        {
            throw new ArgumentException("The price must be greater than zero.", nameof(request));
        }

        var productId = request.ProductId.Trim();
        var storeId = request.StoreId.Trim();

        var alreadyExists = await _dbContext.Prices
            .AnyAsync(price => price.ProductId == productId && price.StoreId == storeId, cancellationToken);

        if (alreadyExists)
        {
            throw new InvalidOperationException(
                $"A price for product '{productId}' in store '{storeId}' already exists.");
        }

        var entity = new PriceEntity
        {
            Id = GeneratePriceId(),
            ProductId = productId,
            StoreId = storeId,
            Price = request.Price,
            UpdatedAt = DateTime.UtcNow
        };

        _dbContext.Prices.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return MapToResponse(entity);
    }
}
