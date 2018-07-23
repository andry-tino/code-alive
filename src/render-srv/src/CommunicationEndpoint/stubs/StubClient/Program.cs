///
/// Andrea Tino - 2018
///

namespace CodeAlive.Communication.Stubs
{
    using System;
    using System.ServiceModel;
    using System.ServiceModel.Description;

    using CodeAlive.Communication;

    public class StubClient
    {
        private const string CommandGeneric = "generic";

        public static void Main(string[] args)
        {
            Console.Write($"Enter the port to use (defaults to {Communicator.DefaultPort})");

            short port = 0;
            var succeeded = short.TryParse(Console.ReadLine(), out port);
            if (!succeeded)
            {
                port = Communicator.DefaultPort;
            }

            var address = $"http://localhost:{port}";
            Console.WriteLine($"Connecting to: {address}...");

            using (var cf = new ChannelFactory<ICommunicationService>(new WebHttpBinding(), address))
            {
                Console.WriteLine($"Connection successful!");

                cf.Endpoint.Behaviors.Add(new WebHttpBehavior());
                ICommunicationService channel = cf.CreateChannel();

                while (true)
                {
                    Console.Write("Input command." + Help);

                    var command = Console.ReadLine();
                    Console.WriteLine($"{command} => {Execute(command, channel)}");
                }
            }
        }

        private static string Execute(string input, ICommunicationService svc)
        {
            if (input == CommandGeneric)
            {
                return svc.EchoWithGet("Hello, world");
            }

            return "None";
        }

        private static string Help => $@"
            => '{CommandGeneric}': Something
        ";
    }
}
