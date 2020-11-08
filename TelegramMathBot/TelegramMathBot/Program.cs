using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace TelegramMathBot
{
    class Program
    {
        static readonly string token = "1495097120:AAHpmNmtzpgF6-_BZe0yXyGdfQYrUdhMokQ";
        static void Main(string[] args)
        {
            var bot = new MathBot(token);
            bot.Start();
        }
    }
}
