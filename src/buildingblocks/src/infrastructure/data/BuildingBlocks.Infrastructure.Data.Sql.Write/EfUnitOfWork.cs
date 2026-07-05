using BuildingBlocks.Infrastructure.Data.Write;
using BuildingBlocks.Integrations.Marten.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingBlocks.Infrastructure.Data.Sql.Write;

public class EfUnitOfWork<TContext> : IUnitOfWork, IAsyncDisposable
    where TContext : BaseCommandDbContext
{
    private readonly TContext _context;

    public EfUnitOfWork(TContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async ValueTask DisposeAsync()
    {
        if (_context != null)
        {
            await _context.DisposeAsync();
        }
    }
}
