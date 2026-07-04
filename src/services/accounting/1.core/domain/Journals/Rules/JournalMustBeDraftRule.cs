using Accounting.Core.Domain.Journals.Enums;
using BuildingBlocks.Domain.Rules;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Core.Domain.Journals.Rules;

internal sealed class JournalMustBeDraftRule : IBusinessRule
{
    private readonly JournalStatus _status;

    internal JournalMustBeDraftRule(JournalStatus status)
    {
        _status = status;
    }

    public string Message => "Journal already posted";

    public bool IsBroken()
        => _status != JournalStatus.Draft;
}