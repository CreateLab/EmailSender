using System.Collections.Generic;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace ConsoleApp2
{
    public class MailClientSingleton
    {
        public async Task SendMessage(IEnumerable<MimeMessage> messages)
        {
            using var client = new SmtpClient();
            // allow less secure app google
            await client.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);

            // Note: only needed if the SMTP server requires authentication
            //TODO: setup ur creditance
            await client.AuthenticateAsync("***", "***");
            foreach (var message in messages)
            {
                await Task.Delay(1000);
                await client.SendAsync(message);
            }

            await client.DisconnectAsync(true);
        }

        public static MimeMessage CreateMessage(string login, string password, string name)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("ITMO", "main.simpleq@gmail.com"));
            message.To.Add(new MailboxAddress(name, $"{login}@niuitmo.ru"));
            message.Subject = "Тема: Доступ на Helios";

            message.Body = new TextPart("plain")
            {
                Text = @$"
                login: {"s" + login}
                password: {password}

                Helios URL: helios.se.ifmo.ru
                Helios SSH port: 2222

                Пример подключения через утилиту ssh в терминале  Unix–подобной ОС:
                $ ssh {"s" + login}@helios.se.ifmo.ru -p 2222
                `$ Password: {password}`
                
                
                "
            };
            return message;
        }
    }
}