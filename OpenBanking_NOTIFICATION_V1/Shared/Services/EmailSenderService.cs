using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Microsoft.Extensions.Configuration;
using OpenBanking_NOTIFICATION_V1.Shared.Events;

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
            Console.WriteLine("=== [LOG] Account Created Notification ===");
            Console.WriteLine($"To: waelgabsigabsi@gmail.com");
            Console.WriteLine($"Subject: New Account Created: {label}");
            Console.WriteLine($"Body:\n🆕 A new account has been created.\n\nAccount ID: {accountId}\nLabel: {label}");
            Console.WriteLine("==========================================");

            await Task.CompletedTask;

            // Uncomment to send real email:
            /*
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
            */
        }

        public async Task SendAccountAttributeCreatedEmail(AccountAttributeCreatedEvent evt)
        {
            Console.WriteLine("=== [LOG] Account Attribute Created Notification ===");
            Console.WriteLine($"To: waelgabsigabsi@gmail.com");
            Console.WriteLine("Subject: New Account Attribute Created");
            Console.WriteLine("Body:");
            Console.WriteLine($"✅ A new attribute was added:\n");
            Console.WriteLine($"ID: {evt.account_attribute_id}");
            Console.WriteLine($"Name: {evt.name}");
            Console.WriteLine($"Value: {evt.value}");
            Console.WriteLine($"Instance Code: {evt.product_instance_code}");
            Console.WriteLine("=====================================================");

            await Task.CompletedTask;

            // Uncomment to send real email:
            /*
            var message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse(_config["Smtp:User"]));
            message.To.Add(MailboxAddress.Parse("waelgabsigabsi@gmail.com"));
            message.Subject = $"New Account Attribute Created";

            message.Body = new TextPart("plain")
            {
                Text = $"✅ A new attribute was added:\n\n" +
                       $"ID: {evt.account_attribute_id}\n" +
                       $"Name: {evt.name}\n" +
                       $"Value: {evt.value}\n" +
                       $"Instance Code: {evt.product_instance_code}"
            };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_config["Smtp:Host"], int.Parse(_config["Smtp:Port"]), SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_config["Smtp:User"], _config["Smtp:Password"]);
            await smtp.SendAsync(message);
            await smtp.DisconnectAsync(true);
            */
        }
         public async Task SendAtmCreatedEmail(AtmCreatedEvent evt)
        {
            Console.WriteLine("=== [LOG] Atm Attribute Created Notification ===");
            Console.WriteLine($"To: System");
            Console.WriteLine("Subject: New Atm  Created");
            Console.WriteLine("Body:");
            Console.WriteLine($"✅ A new atm attribute was added:\n");
            Console.WriteLine($"ID: {evt.atm_id}");
            Console.WriteLine($"Name: {evt.BankId}");
            Console.WriteLine($"Value: {evt.Name}");
            Console.WriteLine("=====================================================");

            await Task.CompletedTask;
        }
          public async Task SendAtmAttributeCreatedEmail(AtmAttributeCreatedEvent evt)
        {
            Console.WriteLine("=== [LOG] Atm Attribute Created Notification ===");
            Console.WriteLine($"To: System");
            Console.WriteLine("Subject: New Atm Attribute Created");
            Console.WriteLine("Body:");
            Console.WriteLine($"✅ A new atm attribute was added:\n");
            
            Console.WriteLine("=====================================================");

            await Task.CompletedTask;
        }

    }
}
