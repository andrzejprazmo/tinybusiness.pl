//-----------------------------------------------------------------------
// <copyright file="IMailSender.cs" company="SEE Software">
//     All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SEE.TinyBusinessMarket.BackEnd.Infrastructure.Mail
{
	using System;
	using System.Collections.Generic;
	using System.Text;
    using System.Threading.Tasks;

    public interface IMailSender
    {
        Task SendAsync(string recipient, string subject, string body);
        Task SendAsync(string recipient, string subject, string body, byte[] data, string fileName);
    }
}
