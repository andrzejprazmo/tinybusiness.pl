//-----------------------------------------------------------------------
// <copyright file="ProFormaContract.cs" company="SEE Software">
//     All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SEE.TinyBusinessMarket.BackEnd.Common.Contract
{
	using System;
	using System.Collections.Generic;
	using System.Text;
	

    public class ProFormaContract
    {
        public Guid OrderId { get; set; }

        public int Number { get; set; }

        public string FullNumber { get; set; }

        public DateTime ExposureDate { get; set; }
    }
}
