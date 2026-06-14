using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Core.Domain.Journals;
public class JournalLine
{
    public Guid Id { get; }
    public Guid AccountId { get; }
    public decimal Debit { get; }
    public decimal Credit { get; }

    public JournalLine(Guid id, Guid accountId, decimal debit, decimal credit)
    {
        Id = id;
        AccountId = accountId;
        Debit = debit;
        Credit = credit;
    }
}
