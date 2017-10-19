using SEE.Framework.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEE.Framework.Core.Extensions
{
    public static class DecimalExtensions
    {
        public static string ToStringify(this decimal value)
        {
            int i = (int)value;
            int f = (int)(value * 100) % 100;
            return $"{i.ToStringify(CurrencyType.Zloty)} {f.ToStringify(CurrencyType.Grosz)}";
        }

        public static decimal ZeroIfNegative(this decimal value)
        {
            if (value < 0)
            {
                return 0;
            }
            return value;
        }
    }
}
