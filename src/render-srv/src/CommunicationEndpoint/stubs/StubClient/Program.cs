/// <summary>
/// Andrea Tino - 2018
/// </summary>

namespace CodeAlive.Communication.Stubs
{
    using System;
    using System.IO;

    public class StubClient
    {
        private const string CommandEcho = "echo";
        private const string CommandNewInstance = "new";
        private const string CommandMessageExchange = "msg";
        private const string CommandNewReference = "ref";
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

            if (input == CommandNewInstance)
            {
                svc.RenderNewCell(new RenderingApi.NewInstanceRenderingRequest() { InstanceId = $"Class@{RndStr}" });
                return "Done";
            }
            if (input.Split(' ').Length == 2 && input.Split(' ')[0] == CommandNewInstance)
            {
                svc.RenderNewCell(new RenderingApi.NewInstanceRenderingRequest() { InstanceId = input.Split(' ')[1] });
                return "Done";
            }

            if (input.Split(' ').Length == 4 && input.Split(' ')[0] == CommandMessageExchange)
            {
                svc.RenderInteraction(new RenderingApi.InteractionRenderingRequest()
                {
                    InvocationName = input.Split(' ')[1],
                    SourceInstanceId = input.Split(' ')[2],
                    DstInstanceId = input.Split(' ')[3]
                });
                return "Done";
            }

            if (input.Split(' ').Length == 3 && input.Split(' ')[0] == CommandNewReference)
            {
                svc.RenderReference(new RenderingApi.GetReferenceRenderingRequest()
                {
                    InstanceId = input.Split(' ')[1],
                    ParentInstanceId = input.Split(' ')[2]
                });
                return "Done";
            }

            if (input == CommandQuit)
            {
                return CommandQuit;
            }

            return "None";
        }

        private static string RndStr => Path.GetRandomFileName().Replace(".", "").Substring(0, 8);

        private static string Help => $@"
            => '{CommandEcho}': Send an echo
        ";
    }
}
