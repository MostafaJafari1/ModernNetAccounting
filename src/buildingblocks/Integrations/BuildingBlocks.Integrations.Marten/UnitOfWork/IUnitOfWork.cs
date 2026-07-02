using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingBlocks.Integrations.Marten.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
