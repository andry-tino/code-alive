/// <summary>
/// Andrea Tino - 2018
/// </summary>

namespace CodeAlive.Communication.RenderingApi
{
    using System;
    using System.ServiceModel;
    using System.ServiceModel.Description;

    public class Client : ICommunicationService, IDisposable
    {
        private ChannelFactory<ICommunicationService> channelFactory;
        private ICommunicationService channel;

        public Client(string hostname = "localhost", short port = 8000)
        {
            this.channelFactory = new ChannelFactory<ICommunicationService>(new WebHttpBinding(), $"http://{hostname}:{port}");
            this.channelFactory.Endpoint.Behaviors.Add(new WebHttpBehavior());
            this.channel = this.channelFactory.CreateChannel();
        }

        public void Dispose()
        {
            this.channelFactory = null;
            this.channel = null;
        }

        #region API

        public string Echo(string message)
        {
            return this.channel.Echo(message);
        }

        public string Echo_Post(string message)
        {
            return this.channel.Echo_Post(message);
        }

        #endregion
    }
}
