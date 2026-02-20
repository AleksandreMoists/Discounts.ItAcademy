using Discounts.Domain.Entities;
using Discounts.Domain.Interfaces;
using Discounts.Persistence.Data;

namespace Discounts.Persistence.Repositories;

public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
    public CategoryRepository(AppDbContext context) : base(context) { }
}
