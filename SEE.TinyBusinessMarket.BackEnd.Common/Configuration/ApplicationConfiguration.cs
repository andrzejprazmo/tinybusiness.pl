//-----------------------------------------------------------------------
// <copyright file="ApplicationConfiguration.cs" company="SEE Software">
//     All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SEE.TinyBusinessMarket.BackEnd.Common.Configuration
{
	using System;
	using System.Collections.Generic;
	using System.Text;
	

    public class ApplicationConfiguration
    {
        public IdentityConfiguration IdentityConfiguration { get; set; }
        public MailConfiguration MailConfiguration { get; set; }
        public PayuConfiguration PayuConfiguration { get; set; }
        public InvoiceConfiguration InvoiceConfiguration { get; set; }
        public DownloadConfiguration DownloadConfiguration { get; set; }
    }
}
