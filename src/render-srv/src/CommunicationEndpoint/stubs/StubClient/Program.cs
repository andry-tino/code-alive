/// <summary>
/// Andrea Tino - 2018
/// </summary>

namespace CodeAlive.Communication.Stubs
{
    using System;

    public class StubClient
    {
        private const string CommandEcho = "echo";
        private const string CommandQuit = "quit";

        private const short DefaultPort = 8000;

        public static void Main(string[] args)
        {
            Console.Write($"Enter the port to use (defaults to {DefaultPort})");

            short port = 0;
            var succeeded = short.TryParse(Console.ReadLine(), out port);
            if (!succeeded)
            {
                port = DefaultPort;
            }

            var address = $"http://localhost:{port}";
            Console.WriteLine($"Connecting to: {address}...");

            using (var channel = new RenderingApi.Client("localhost", port))
            {
                Console.WriteLine($"Connection successful!");
                
                while (true)
                {
                    Console.Write("Command: ");

                    var command = Console.ReadLine();
                    var response = Execute(command, channel).Trim();

                    Console.WriteLine($"{command} => {response}");

                    if (response == CommandQuit)
                    {
                        break;
                    }
                }
            }
        }

        private static string Execute(string input, RenderingApi.ICommunicationService svc)
        {
            if (input == CommandEcho)
            {
                return svc.Echo(new RenderingApi.DiagnosticRenderingRequest() { Content = "This is an echo" });
            }

            if (input == CommandQuit)
            {
                return CommandQuit;
            }

            return "None";
        }

        private static string Help => $@"
            => '{CommandEcho}': Send an echo
        ";
    }
}
