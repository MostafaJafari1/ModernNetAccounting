using BuildingBlocks.Infrastructure.Data.Write;
using Marten;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingBlocks.Integrations.Marten.UnitOfWork
{
    public class MartenUnitOfWork : IUnitOfWork, IAsyncDisposable
    {
        private readonly IDocumentSession _session;

        public MartenUnitOfWork(IDocumentSession session)
        {
            _session = session;
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _session.SaveChangesAsync(cancellationToken);
        }

        public async ValueTask DisposeAsync()
        {
            if (_session != null)
            {
                await _session.DisposeAsync();
            }
        }
    }
}
