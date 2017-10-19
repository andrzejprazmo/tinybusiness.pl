//-----------------------------------------------------------------------
// <copyright file="PaymentUpdateModel.cs" company="SEE Software">
//     All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SEE.TinyBusinessMarket.BackEnd.Common.UpdateModel
{
    using SEE.Framework.Core.DTO;
    using SEE.TinyBusinessMarket.BackEnd.Common.Contract;
    using System;
	using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
	

    public class PaymentUpdateModel
    {
        public ProductContract Product { get; set; }

        public CustomerUpdateModel Customer { get; set; }

        public string RedirectUrl { get; set; }
    }
}
