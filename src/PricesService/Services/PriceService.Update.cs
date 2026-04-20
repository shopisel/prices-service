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
        if (request.Price <= 0)
        {
            throw new ArgumentException("The price must be greater than zero.", nameof(request));
        }

        if (request.Sale is <= 0)
        {
            throw new ArgumentException("The sale must be greater than zero when informed.", nameof(request));
        }

        var entity = await _dbContext.Prices
            .FirstOrDefaultAsync(price => price.Id == id.Trim(), cancellationToken);

        if (entity is null)
        {
            return null;
        }

        entity.Price = request.Price;
        entity.Sale = request.Sale;
        entity.SaleDate = request.SaleDate;
        entity.UpdatedAt = DateTime.UtcNow;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return MapToResponse(entity);
    }
}
