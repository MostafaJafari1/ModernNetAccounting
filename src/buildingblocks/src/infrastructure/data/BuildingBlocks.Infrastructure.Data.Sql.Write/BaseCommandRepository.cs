using BuildingBlocks.Contracts.Infrastructure.Data.Sql;
using BuildingBlocks.Domain.Primitives.AggregateRoots;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BuildingBlocks.Infrastructure.Data.Sql.Write;

public class BaseCommandRepository<TEntity, TId> : IBaseCommandRepository<TEntity, TId> where TEntity : AggregateRoot
{
    protected readonly DbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public BaseCommandRepository(DbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = _context.Set<TEntity>();
    }

    public virtual async Task<TEntity> GetByIdAsync(TId id, params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = _dbSet;

        if (includes != null)
        {
            query = includes.Aggregate(query, (current, include) => current.Include(include));
        }

        return await query.FirstOrDefaultAsync(e => Equals(e.Id, id));
    }

    public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        await _dbSet.AddAsync(entity, cancellationToken);
    }

    public virtual void Update(TEntity entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        _dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }

    public virtual void DeleteSoftly(TEntity entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        entity.Delete();

        _dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }
}