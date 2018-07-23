///
/// Andrea Tino - 2018
///

namespace CodeAlive.Communication.Stubs
{
    using System;

    using CodeAlive.Communication;
    using CodeAlive.Communication.Eventing;

    public class StubServer
    {
        public static void Main(string[] args)
        {
            Console.Write($"Enter the port to use (defaults to {Communicator.DefaultPort})");

            short port = 0;
            var succeeded = short.TryParse(Console.ReadLine(), out port);
            if (!succeeded)
            {
                port = Communicator.DefaultPort;
            }

            Console.WriteLine($"Creating communicator server listening on port: {port}...");
            var communicator = new Communicator(port);
            communicator.EventOccurred += OnEventOccurred;
            communicator.Initialize(); // This starts the communicator

            Console.WriteLine("Ready!");

            Console.WriteLine("Press a key to kill the server!");
            Console.ReadLine();
        }

        private static void OnEventOccurred(RenderingEvent e)
        {
            Console.WriteLine($"Event occurred: {e.ToString()}");
        }
    }
}
