using Discounts.Domain.Entities;
using Discounts.Domain.Interfaces;
using Discounts.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Discounts.Persistence.Repositories;

public class MerchantRepository : BaseRepository<Merchant>, IMerchantRepository
{
    public MerchantRepository(AppDbContext context) : base(context) { }

    public async Task<Merchant?> GetByEmail(string email, CancellationToken ct = default)
    {
        return await DbSet
            .Include(m => m.User)
            .FirstOrDefaultAsync(m => m.User.Email == email, ct);
    }

    public async Task<IEnumerable<Merchant>> GetActiveMerchants(CancellationToken ct = default)
    {
        return await DbSet
            .Include(m => m.User)
            .Where(m => m.IsActive)
            .ToListAsync(ct);
    }
}
