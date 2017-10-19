using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEE.Framework.Core.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Parse date or datetime string and returns <see cref="DateTime"/> or null if parsing failed.
        /// </summary>
        /// <param name="value">String data to parse.</param>
        /// <returns><see cref="DateTime"/> or null if parsing failed</returns>
        public static DateTime? ToDateOrNull(this string value)
        {
            DateTime date = new DateTime();
            if (!string.IsNullOrWhiteSpace(value) && DateTime.TryParse(value, out date))
            {
                return date;
            }
            return null;

        }

        public static string ToJsonCamel(this string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                value = char.ToLower(value[0]) + value.Substring(1);
            }
            return value;
        }
    }
}
