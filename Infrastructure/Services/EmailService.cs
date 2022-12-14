using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        #region fields

        private readonly IConfiguration _configuration;
        private string apiKey = null;
        private SendGridClient client = null;

        #endregion fields

        #region ctor

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
            apiKey = _configuration["SENDGRID:API_KEY"];
            client = new SendGridClient(apiKey);
        }

        #endregion ctor

        #region methods

        /// <summary>
        ///KHÔNG HOẠT ĐỘNG !!! ĐỪNG DÙNG
        /// </summary>
        public void SendByMailKit(string to, string subject, string html, string from = null)
        {
            // create message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(from ?? _configuration["MAIL_CONFIG:EMAIL_FROM"]));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = html };

            // send email
            using var smtp = new SmtpClient();
            smtp.Connect(_configuration["MAIL_CONFIG:SMTP_HOST"], int.Parse(_configuration["MAIL_CONFIG:SMTP_PORT"]), SecureSocketOptions.StartTls);
            smtp.Authenticate(_configuration["MAIL_CONFIG:SMTP_USER"], _configuration["MAIL_CONFIG:SMTP_PASSWORD"]);
            smtp.Send(email);
            smtp.Disconnect(true);
        }

        /// <summary>
        /// Send email to user by using Sendgrid API
        /// </summary>
        /// <param name="to">Received Email</param>
        /// <param name="subject">Email Subject</param>
        /// <param name="data">Dynamic data for template</param>
        /// <param name="templateId">Template Id</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public async Task SendBySendgrid(string to, string subject, object data, string templateId)
        {
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(_configuration["SENDGRID:SENDER_EMAIL"])
            };
            msg.SetSubject(subject);
            msg.AddTo(new EmailAddress(to));
            msg.SetTemplateId(templateId);
            msg.SetTemplateData(data);
            await client.SendEmailAsync(msg);
        }

        #endregion methods
    }
}