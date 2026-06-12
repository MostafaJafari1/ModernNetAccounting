using BuildingBlocks.Infrastructure.UnitOfWork;
using Marten;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingBlocks.Integration.Marten.UnitOfWork
{
    public class MartenUnitOfWork : IUnitOfWork
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
    }
}
