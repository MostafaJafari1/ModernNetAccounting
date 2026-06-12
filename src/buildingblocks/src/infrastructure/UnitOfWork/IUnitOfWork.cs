using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingBlocks.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
