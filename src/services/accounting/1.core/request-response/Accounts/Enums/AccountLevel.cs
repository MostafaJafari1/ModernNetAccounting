using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Core.RequestResponse.Accounts.Enums;
public enum AccountLevel
{
    Group = 1,       // گروه حساب
    General = 2,     // کل
    Subsidiary = 3,  // معین
    Detail = 4       // تفضیلی
}