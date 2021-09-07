using System;
using System.Collections.Concurrent;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace ConsoleApp2
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var period = 1;
            var status = true;
            while (status)
            {
                try
                {
                    var files = Directory.GetFiles("./data");
                    var mailClientSingleton = new MailClientSingleton();
                    foreach (var file in files)
                    {
                        var enumerable = (await File.ReadAllLinesAsync(file)).Select(CreditanceDTO.Create)
                            .Select(c => MailClientSingleton.CreateMessage(c.Login, c.Password, c.Name));
                        await mailClientSingleton.SendMessage(enumerable).ConfigureAwait(false);
                        await Task.Delay(10000);
                        Console.WriteLine(file);
                        File.Delete(file);
                        period = 1;
                    }

                    status = false;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    await Task.Delay(period * 60000);
                    period++;
                }
            }
        }
    }
}