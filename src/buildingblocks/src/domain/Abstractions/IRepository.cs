namespace BuildingBlocks.Domain.Abstractions;

public interface IRepository<TEntity>
    where TEntity : IAggregateRoot
{
    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    void Update(TEntity entity);

    void Delete(TEntity entity);

    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
}
