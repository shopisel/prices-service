using PricesService.Contracts;

namespace PricesService.Services;

public interface IPriceService
{
    Task<IEnumerable<PriceResponse>> GetByProductIdAsync(string productId, CancellationToken cancellationToken = default);
    Task<PriceResponse?> GetByProductAndStoreAsync(string productId, string storeId, CancellationToken cancellationToken = default);
    Task<PriceResponse> CreateAsync(CreatePriceRequest request, CancellationToken cancellationToken = default);
    Task<PriceResponse?> UpdateAsync(string id, UpdatePriceRequest request, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(string id, CancellationToken cancellationToken = default);
}
