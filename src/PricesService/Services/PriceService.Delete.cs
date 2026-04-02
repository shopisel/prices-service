using Microsoft.EntityFrameworkCore;

namespace PricesService.Services;

public partial class PriceService
{
    public async Task<bool> DeleteAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.Prices
            .FirstOrDefaultAsync(price => price.Id == id.Trim(), cancellationToken);

        if (entity is null)
        {
            return false;
        }

        _dbContext.Prices.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}
