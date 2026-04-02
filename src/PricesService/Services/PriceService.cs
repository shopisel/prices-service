using PricesService.Contracts;
using PricesService.Data;

namespace PricesService.Services;

public partial class PriceService(PricesServiceDbContext dbContext) : IPriceService
{
    private readonly PricesServiceDbContext _dbContext = dbContext;

    private static string GeneratePriceId() => $"price_{Guid.NewGuid():N}";

    private static PriceResponse MapToResponse(Data.Entities.PriceEntity entity) =>
        new(entity.Id, entity.ProductId, entity.StoreId, entity.Price, entity.UpdatedAt);
}
