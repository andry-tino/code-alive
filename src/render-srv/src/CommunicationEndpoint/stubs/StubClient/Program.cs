﻿/// <summary>
/// Andrea Tino - 2018
/// </summary>

namespace CodeAlive.Communication.Stubs
{
    using System;
    using System.ServiceModel;
    using System.ServiceModel.Description;

    using CodeAlive.Communication;

    public class StubClient
    {
        private const string CommandGeneric = "generic";
        private const string CommandQuit = "quit";

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

        private static string Execute(string input, ICommunicationService svc)
        {
            if (input == CommandGeneric)
            {
                return svc.EchoWithGet("Hello, world");
            }

            if (input == CommandQuit)
            {
                return CommandQuit;
            }

            return "None";
        }

        private static string Help => $@"
            => '{CommandGeneric}': Something
        ";
    }
}