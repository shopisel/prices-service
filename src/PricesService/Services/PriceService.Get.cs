using PricesService.Contracts;
using Microsoft.EntityFrameworkCore;

namespace PricesService.Services;

public partial class PriceService
{
    public async Task<IEnumerable<PriceResponse>> GetByProductIdAsync(
        string productId,
        CancellationToken cancellationToken = default)
    {
        var normalizedProductId = productId.Trim();

        var prices = await _dbContext.Prices
            .AsNoTracking()
            .Where(price => price.ProductId == normalizedProductId)
            .OrderBy(price => price.StoreId)
            .ToListAsync(cancellationToken);

        return prices.Select(MapToResponse);
    }

    public async Task<PriceResponse?> GetByProductAndStoreAsync(
        string productId,
        string storeId,
        CancellationToken cancellationToken = default)
    {
        var normalizedProductId = productId.Trim();
        var normalizedStoreId = storeId.Trim();

        var price = await _dbContext.Prices
            .AsNoTracking()
            .FirstOrDefaultAsync(
                price => price.ProductId == normalizedProductId && price.StoreId == normalizedStoreId,
                cancellationToken);

        return price is null ? null : MapToResponse(price);
    }
}
