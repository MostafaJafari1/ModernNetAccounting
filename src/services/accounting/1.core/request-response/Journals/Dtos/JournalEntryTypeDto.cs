using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Core.RequestResponse.Journals.Dtos;
public enum JournalEntryTypeDto
{
    General = 1,
    Adjustment = 2,
    Reversal = 3,
    Opening = 4,
    Closing = 5
}