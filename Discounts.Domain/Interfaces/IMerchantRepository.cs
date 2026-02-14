using Discounts.Domain.Entities;

namespace Discounts.Domain.Interfaces;

public interface IMerchantRepository : IBaseRepository<Merchant>
{
    Task<Merchant?> GetByEmail(string email, CancellationToken ct = default);
    Task<IEnumerable<Merchant>> GetActiveMerchants(CancellationToken ct = default);
}