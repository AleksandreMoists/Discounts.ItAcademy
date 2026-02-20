using Discounts.Domain.Interfaces;
using Discounts.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Discounts.Persistence.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    protected readonly AppDbContext Context;
    protected readonly DbSet<T> DbSet;

    public BaseRepository(AppDbContext context)
    {
        Context = context;
        DbSet = context.Set<T>();
    }

    public async Task<T> GetById(int id, CancellationToken ct = default)
    {
        return (await DbSet.FindAsync(new object[] { id }, ct))!;
    }

    public async Task<IEnumerable<T>> GetAll(CancellationToken ct = default)
    {
        return await DbSet.ToListAsync(ct);
    }

    public async Task<T> Add(T entity, CancellationToken ct = default)
    {
        await DbSet.AddAsync(entity, ct);
        await Context.SaveChangesAsync(ct);
        return entity;
    }

    public async Task<bool> Update(T entity)
    {
        DbSet.Update(entity);
        return await Context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Delete(T entity)
    {
        DbSet.Remove(entity);
        return await Context.SaveChangesAsync() > 0;
    }
}
