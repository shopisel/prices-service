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

        if (string.IsNullOrWhiteSpace(request.PriceText))
        {
            throw new ArgumentException("The priceText is required.", nameof(request));
        }

        var productId = request.ProductId.Trim();
        var storeId = request.StoreId.Trim();
        var priceText = request.PriceText.Trim();
        var saleText = string.IsNullOrWhiteSpace(request.SaleText) ? null : request.SaleText.Trim();
        var quantityText = string.IsNullOrWhiteSpace(request.QuantityText) ? null : request.QuantityText.Trim();
        var unitPriceText = string.IsNullOrWhiteSpace(request.UnitPriceText) ? null : request.UnitPriceText.Trim();

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
            PriceText = priceText,
            SaleText = saleText,
            QuantityText = quantityText,
            UnitPriceText = unitPriceText,
            SaleDate = request.SaleDate,
            UpdatedAt = DateTime.UtcNow
        };

        _dbContext.Prices.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return MapToResponse(entity);
    }
}
