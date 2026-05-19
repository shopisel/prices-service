using PricesService.Contracts;
using Microsoft.EntityFrameworkCore;

namespace PricesService.Services;

public partial class PriceService
{
    public async Task<PriceResponse?> UpdateAsync(
        string id,
        UpdatePriceRequest request,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(request.PriceText))
        {
            throw new ArgumentException("The priceText is required.", nameof(request));
        }

        var entity = await _dbContext.Prices
            .FirstOrDefaultAsync(price => price.Id == id.Trim(), cancellationToken);

        if (entity is null)
        {
            return null;
        }

        entity.PriceText = request.PriceText.Trim();
        entity.SaleText = string.IsNullOrWhiteSpace(request.SaleText) ? null : request.SaleText.Trim();
        entity.QuantityText = string.IsNullOrWhiteSpace(request.QuantityText) ? null : request.QuantityText.Trim();
        entity.UnitPriceText = string.IsNullOrWhiteSpace(request.UnitPriceText) ? null : request.UnitPriceText.Trim();
        entity.SaleDate = request.SaleDate;
        entity.UpdatedAt = DateTime.UtcNow;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return MapToResponse(entity);
    }
}
