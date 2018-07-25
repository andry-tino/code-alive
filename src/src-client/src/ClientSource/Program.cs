/// <summary>
/// Andrea Tino - 2018
/// </summary>

namespace CodeAlive.ClientSource
{
    using System;

    using CodeAlive.Communication.RenderingApi;

    public class Program
    {
        private static Client client;

        public static void Main(string[] args)
        {
            client = new Client("localhost", 8000);
            var src = CreateSource(client);

            src.Run();
        }

        private static ISource CreateSource(ICommunicationService svc)
        {
            return new Basic.SourceRunner(svc, 3000);
        }
    }
}
