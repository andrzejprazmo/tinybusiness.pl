//-----------------------------------------------------------------------
// <copyright file="MailConfiguration.cs" company="SEE Software">
//     All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SEE.TinyBusinessMarket.BackEnd.Common.Configuration
{
	using System;
	using System.Collections.Generic;
	using System.Text;
	

    public class MailConfiguration
    {
        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
        public bool SmtpUseSsl { get; set; }
        public string SmtpUser { get; set; }
        public string SmtpPassword { get; set; }

        public string SenderName { get; set; }
        public string SenderAddress { get; set; }
    }
}
