using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace CommunicationClient
{
    public class Client
    {
        public static void Main()
        {
            using (var cf = new ChannelFactory<ICommunicationService>(new WebHttpBinding(), "http://localhost:8000"))
            {
                cf.Endpoint.Behaviors.Add(new WebHttpBehavior());
                ICommunicationService channel = cf.CreateChannel();

                Console.WriteLine("Calling EchoWithGet via HTTP GET: ");
                string response = channel.EchoWithGet("Hello, world");
                Console.WriteLine("Output: {0}", response);
            }
        }
    }
}
