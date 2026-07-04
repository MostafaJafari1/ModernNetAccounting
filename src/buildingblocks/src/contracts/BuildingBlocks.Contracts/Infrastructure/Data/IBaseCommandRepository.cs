using BuildingBlocks.Domain.Primitives.AggregateRoots;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BuildingBlocks.Contracts.Infrastructure.Data;
public interface IBaseCommandRepository<TEntity, TId> where TEntity : AggregateRoot
{
    Task<TEntity> GetByIdAsync(TId id, params Expression<Func<TEntity, object>>[] includes);

    Task AddAsync(TEntity entity);

    void Update(TEntity entity);

    //Soft delete
    void DeleteSoftly(TEntity entity);
}