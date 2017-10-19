//-----------------------------------------------------------------------
// <copyright file="TokenContract.cs" company="SEE Software">
//     All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SEE.TinyBusinessMarket.BackEnd.Common.Contract
{
	using System;
	using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
	

    public class TokenContract
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public string Data { get; set; }
        public bool Active { get; set; }

        [DisplayFormat(DataFormatString = "yyyy-MM-dd HH:mm")]
        public DateTime Created { get; set; }

        [DisplayFormat(DataFormatString = "yyyy-MM-dd HH:mm")]
        public DateTime Expires { get; set; }

        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string CustomerEmail { get; set; }
    }
}
