using System;

namespace Echo.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Server WebSocketServer = new Server();

            WebSocketServer.Start("http://+:1337/echo/");
            Console.WriteLine($"Press any key to exit...{Environment.NewLine}");
            Console.ReadKey();
        }
    }
}
