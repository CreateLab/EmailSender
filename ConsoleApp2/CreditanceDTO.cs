using System;

namespace ConsoleApp2
{
    public class CreditanceDTO
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }

        public static CreditanceDTO Create(string line)
        {
            var split = line.Split(":",StringSplitOptions.RemoveEmptyEntries);
            return new CreditanceDTO
            {
                Login = split[0].Remove(0,1).Trim(),
                Password = split[1].Trim(),
                Name = split[2].Trim()
            };
        }
    }
}