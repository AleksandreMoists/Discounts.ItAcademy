namespace Discounts.Domain.Interfaces;

public interface IBaseRepository<T> where T : class
{
    Task<T> GetById(int id, CancellationToken ct = default);
    Task<IEnumerable<T>> GetAll(CancellationToken ct = default);
    Task<T> Add(T entity, CancellationToken ct = default);
    Task<bool> Update(T entity);
    Task<bool> Delete(T entity);
}