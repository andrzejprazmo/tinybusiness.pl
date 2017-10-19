//-----------------------------------------------------------------------
// <copyright file="InvoiceNumberBuilder.cs" company="SEE Software">
//     All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SEE.TinyBusinessMarket.BackEnd.Common.Helpers
{
	using System;
	using System.Collections.Generic;
	using System.Text;
	

    public class InvoiceNumberBuilder
    {
        public static string Build(int number, DateTime now)
        {
            return $"{number}/{now.Year}";
        }
    }
}
