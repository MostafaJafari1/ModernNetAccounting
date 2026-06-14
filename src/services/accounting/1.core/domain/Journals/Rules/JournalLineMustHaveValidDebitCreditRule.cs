using BuildingBlocks.Domain.Rules;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Core.Domain.Journals.Rules;
public class JournalLineMustHaveValidDebitCreditRule : IBusinessRule
{
    private readonly decimal _debit;
    private readonly decimal _credit;

    public JournalLineMustHaveValidDebitCreditRule(decimal debit, decimal credit)
    {
        _debit = debit;
        _credit = credit;
    }

    public string Message => "Invalid debit/credit combination";

    public bool IsBroken()
    {
        return (_debit <= 0 && _credit <= 0)
            || (_debit > 0 && _credit > 0);
    }
}