using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Microsoft.Extensions.Configuration;

namespace OpenBanking_NOTIFICATION_V1.Shared.Services
{
    public class EmailSenderService
    {
        private readonly IConfiguration _config;

        public EmailSenderService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendAccountCreatedEmail(string accountId, string label)
{
    var message = new MimeMessage();
    message.From.Add(MailboxAddress.Parse(_config["Smtp:User"]));
    message.To.Add(MailboxAddress.Parse("waelgabsigabsi@gmail.com"));
    message.Subject = $"New Account Created: {label}";

    message.Body = new TextPart("plain")
    {
        Text = $"🆕 A new account has been created.\n\nAccount ID: {accountId}\nLabel: {label}"
    };

    using var smtp = new SmtpClient();
    await smtp.ConnectAsync(_config["Smtp:Host"], int.Parse(_config["Smtp:Port"]), SecureSocketOptions.StartTls);
    await smtp.AuthenticateAsync(_config["Smtp:User"], _config["Smtp:Password"]);
    await smtp.SendAsync(message);
    await smtp.DisconnectAsync(true);
}

    }
}
