/// <summary>
/// Andrea Tino - 2018
/// </summary>

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
            communicator.DiagnosticOccurred += OnDiagnosticOccurred;
            communicator.NewCellOccurred += OnNewCellOccurred;
            communicator.MessageExchangeOccurred += OnMessageExchangeOccurred;
            communicator.NewReferenceOccurred += OnNewReferenceOccurred;
            communicator.Initialize(); // This starts the communicator

            Console.WriteLine("Ready!");

            Console.WriteLine("Press ENTER key to kill the server!");
            Console.ReadLine();
        }

        #region Events

        private static void OnDiagnosticOccurred(DiagnosticRenderingEvent e)
        {
            Console.WriteLine($"Diagnostic occurred - Content: {e.Content}");
        }

        private static void OnNewCellOccurred(NewCellRenderingEvent e)
        {
            Console.WriteLine($"New-Cell occurred - Id: {e.Id}");
        }

        private static void OnMessageExchangeOccurred(MessageExchangeRenderingEvent e)
        {
            Console.WriteLine($"Message-Exchange occurred - Name: {e.InvocationName}, Src: {e.SourceId}, Dst: {e.DestinationId}");
        }

        private static void OnNewReferenceOccurred(NewReferenceRenderingEvent e)
        {
            Console.WriteLine($"New-Reference occurred - Instance: {e.InstanceId}, Parent: {e.ParentId}");
        }

        #endregion
    }
}
