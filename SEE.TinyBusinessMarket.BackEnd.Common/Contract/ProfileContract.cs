//-----------------------------------------------------------------------
// <copyright file="ProfileContract.cs" company="SEE Software">
//     All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SEE.TinyBusinessMarket.BackEnd.Common.Contract
{
	using System;
	using System.Collections.Generic;
	using System.Text;
	

    public class ProfileContract
    {
        public CustomerContract Customer { get; set; }
        public List<LicenceContract> Licences { get; set; }
        public List<InvoiceContract> Invoices { get; set; }
    }
}
