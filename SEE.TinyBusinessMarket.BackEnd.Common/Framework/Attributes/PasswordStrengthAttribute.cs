//-----------------------------------------------------------------------
// <copyright file="PasswordStrengthAttribute.cs" company="SEE Software">
//     All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace System.ComponentModel.DataAnnotations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;


    public class PasswordStrengthAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string password = value as string;
            if (string.IsNullOrWhiteSpace(password)) return true;
            int score = 1;

            if (password.Length >= 8) score++;
            if (Regex.IsMatch(password, @"\d+"))
            {
                score++;
            }
            if (Regex.IsMatch(password, @"[A-Z]"))
            {
                score++;
            }
            if (Regex.IsMatch(password, @"[!@#\$%\^&\*\?_~\-\(\);\.\+:]+"))
            {
                score++;
            }

            return score >= 5;
        }
    }
}
