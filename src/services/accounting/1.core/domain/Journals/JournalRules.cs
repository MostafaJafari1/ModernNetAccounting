using Accounting.Core.Domain.Journals.Rules;
using BuildingBlocks.Domain.Rules;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Core.Domain.Journals;

public static class JournalRules
{
    public static IBusinessRule MustBeDraft(JournalStatus status)
        => new JournalMustBeDraftRule(status);

    public static IBusinessRule ValidDebitCredit(decimal debit, decimal credit)
        => new JournalLineMustHaveValidDebitCreditRule(debit, credit);

    public static IBusinessRule MustBeBalanced(decimal totalDebit, decimal totalCredit)
        => new JournalMustBeBalancedRule(totalDebit, totalCredit);

    public static IBusinessRule MustHaveAtLeastOneLine(int lineCount)
    => new JournalMustHaveAtLeastOneLineRule(lineCount);
}