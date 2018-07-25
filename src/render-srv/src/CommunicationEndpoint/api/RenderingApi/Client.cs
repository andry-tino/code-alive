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

        public string Echo(DiagnosticRenderingRequest message)
        {
            return this.channel.Echo(message);
        }

        public void RenderInteraction(InteractionRenderingRequest message)
        {
            this.channel.RenderInteraction(message);
        }

        public void RenderReference(GetReferenceRenderingRequest message)
        {
            this.channel.RenderReference(message);
        }

        public void RenderNewCell(NewInstanceRenderingRequest message)
        {
            this.channel.RenderNewCell(message);
        }

        #endregion
    }
}
