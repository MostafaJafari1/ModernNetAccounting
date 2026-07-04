using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Core.RequestResponse.Accounts.Enums;

public enum AccountNature
{
    Debit = 1,          // بدهکار
    Credit = 2,         // بستانکار
    Both = 3,           // بدهکار/بستانکار (دوگانه)
    None = 4            // بدون ماهیت (حساب‌های انتظامی/آماری)
}