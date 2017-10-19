//-----------------------------------------------------------------------
// <copyright file="Tokenizer.cs" company="SEE Software">
//     All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SEE.TinyBusinessMarket.BackEnd.Common.Helpers
{
	using System;
	using System.Collections.Generic;
	using System.Text;
	

    public class Tokenizer
    {
        public static string Token()
        {
            return Guid.NewGuid().ToString().Replace("-", string.Empty);
        }
    }
}
