using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.ComponentModel.DataAnnotations
{
    public class DifferentFromZeroAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }
            if (value.GetType() == typeof(int) && int.Parse(value.ToString()) != 0)
            {
                return true;
            }
            if (value.GetType() == typeof(decimal) && decimal.Parse(value.ToString()) != 0)
            {
                return true;
            }
            if (float.Parse(value.ToString()) != 0)
            {
                return true;
            }

            return false;
        }
    }
}
