//-----------------------------------------------------------------------
// <copyright file="IdentityConfiguration.cs" company="SEE Software">
//     All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SEE.TinyBusinessMarket.BackEnd.Common.Configuration
{
	using System;
	using System.Collections.Generic;
	using System.Text;
	

    public class IdentityConfiguration
    {
        public int PasswordMinLength { get; set; } = 8;
        public bool PasswordBigLetterRequired { get; set; } = false;
        public bool PasswordSpecialCharRequired { get; set; } = false;
        public bool PasswordDigitRequired { get; set; } = false;
        public int TokenHoursExpires { get; set; } = 24;
    }
}
