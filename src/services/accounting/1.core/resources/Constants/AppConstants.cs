using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Core.Resources.Constants;

public static class AppConstants
{
    public static class Journal
    {
        public const int DescriptionMaxLength = 500;
    }

    public static class Account
    {
        public const int CodeMaxLength = 20; 
        public const int NameMaxLength = 250;
    }
}
