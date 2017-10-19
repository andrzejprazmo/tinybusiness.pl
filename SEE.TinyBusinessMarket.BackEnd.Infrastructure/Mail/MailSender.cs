//-----------------------------------------------------------------------
// <copyright file="MailSender.cs" company="SEE Software">
//     All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SEE.TinyBusinessMarket.BackEnd.Infrastructure.Mail
{
    using MailKit.Net.Smtp;
    using Microsoft.Extensions.Options;
    using MimeKit;
    using MimeKit.Text;
    using SEE.TinyBusinessMarket.BackEnd.Common.Configuration;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public class MailSender : IMailSender
    {
        private readonly ApplicationConfiguration _configuration;
        public MailSender(IOptions<ApplicationConfiguration> options)
        {
            _configuration = options.Value;
        }

        public async Task SendAsync(string recipient, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_configuration.MailConfiguration.SenderName, _configuration.MailConfiguration.SenderAddress));
            message.To.Add(new MailboxAddress(recipient));
            message.Subject = subject;
            message.Body = new TextPart(TextFormat.Html)
            {
                Text = body,
            };
            await SendAsync(message);
        }

        public async Task SendAsync(string recipient, string subject, string body, byte[] data, string fileName)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_configuration.MailConfiguration.SenderName, _configuration.MailConfiguration.SenderAddress));
            message.To.Add(new MailboxAddress(recipient));
            var builder = new BodyBuilder
            {
                HtmlBody = body,
            };
            builder.Attachments.Add(fileName, data);
            message.Subject = subject;
            message.Body = builder.ToMessageBody();
            await SendAsync(message);
        }

        private async Task SendAsync(MimeMessage message)
        {
            using (var smtp = new SmtpClient())
            {
                smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;
                smtp.Connect(_configuration.MailConfiguration.SmtpHost, _configuration.MailConfiguration.SmtpPort, _configuration.MailConfiguration.SmtpUseSsl);
                smtp.AuthenticationMechanisms.Remove("XOAUTH2");
                smtp.Authenticate(_configuration.MailConfiguration.SmtpUser, _configuration.MailConfiguration.SmtpPassword);
                await smtp.SendAsync(message);
                smtp.Disconnect(true);
            }
        }
    }
}
