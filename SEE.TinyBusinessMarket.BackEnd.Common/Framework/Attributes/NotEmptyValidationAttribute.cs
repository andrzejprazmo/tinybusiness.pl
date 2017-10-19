//-----------------------------------------------------------------------
// <copyright file="NotEmptyValidationAttribute.cs" company="SEE Software">
//     All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace System.ComponentModel.DataAnnotations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;


    public class NotEmptyValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null) return false;

            var valueType = value.GetType();
            if (valueType == typeof(Guid))
            {
                return (Guid)value != Guid.Empty;
            }
            if (valueType == typeof(int))
            {
                return (int)value > 0;
            }
            if (valueType == typeof(DateTime))
            {
                return (DateTime)value != DateTime.MinValue;
            }
            if (valueType == typeof(string))
            {
                return !string.IsNullOrWhiteSpace((string)value);
            }
            return true;
        }
    }
}
