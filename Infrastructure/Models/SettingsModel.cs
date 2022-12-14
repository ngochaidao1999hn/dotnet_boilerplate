using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class SettingsModel
    {
        public ConnectionStrings ConnectionStrings { get; set; }
        public Logging Logging { get; set; }
        public string AllowedHosts { get; set; }
        public JWT JWT { get; set; }
        public MAILCONFIG MAIL_CONFIG { get; set; }
        public SENDGRID SENDGRID { get; set; }
    }

    public class ConnectionStrings
    {
        public string MyConnectionString { get; set; }
    }

    public class JWT
    {
        public string ValidAudience { get; set; }
        public string ValidIssuer { get; set; }
        public string Secret { get; set; }
    }

    public class Logging
    {
        public LogLevel LogLevel { get; set; }
    }

    public class LogLevel
    {
        public string Default { get; set; }

        [JsonProperty("Microsoft.AspNetCore")]
        public string MicrosoftAspNetCore { get; set; }
    }

    public class MAILCONFIG
    {
        public string SMTP_HOST { get; set; }
        public int SMTP_PORT { get; set; }
        public string SMTP_USER { get; set; }
        public string SMTP_PASSWORD { get; set; }
        public string EMAIL_FROM { get; set; }
    }

    public class SENDGRID
    {
        public string API_KEY { get; set; }
        public string SENDER_EMAIL { get; set; }
    }
}