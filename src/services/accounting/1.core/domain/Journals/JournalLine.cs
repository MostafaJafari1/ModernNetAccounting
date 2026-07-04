using BuildingBlocks.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Core.Domain.Journals;

public class JournalLine : Entity
{
    public Guid AccountId { get; }
    public decimal Debit { get; }
    public decimal Credit { get; }

    public JournalLine(Guid id, Guid accountId, decimal debit, decimal credit):base(id)
    {
        AccountId = accountId;
        Debit = debit;
        Credit = credit;
    }
}
