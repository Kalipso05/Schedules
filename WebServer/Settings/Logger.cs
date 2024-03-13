using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebServer.Settings
{
    public static class Logger
    {
        public static void Log(string message, ConsoleColor consoleColor = ConsoleColor.Green, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            Console.ForegroundColor = consoleColor;
            Console.WriteLine($"{DateTime.Now} - {message}. Status code: {statusCode}");
        }
    }
}
