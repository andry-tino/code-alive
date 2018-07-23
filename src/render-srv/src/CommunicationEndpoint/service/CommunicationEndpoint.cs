/// <summary>
/// Andrea Tino - 2018
/// </summary>

namespace CodeAlive.Communication
{
    using System;
    using System.ServiceModel;
    using System.ServiceModel.Web;

    /// <summary>
    /// Holds the endpoint logic.
    /// </summary>
    internal class CommunicationEndpoint : IDisposable
    {
        public const short DefaultPort = 8000;

        private short port;
        private CommunicationService service;
        private ServiceHost host;

        /// <summary>
        /// Creates a new instance of the <see cref="CommunicationEndpoint"/> class.
        /// </summary>
        /// <param name="port">The port to use. Defaults to <see cref="DefaultPort"/></param>
        public CommunicationEndpoint(short port = DefaultPort)
        {
            this.port = port;
        }

        public void Start()
        {
            this.EnsureInitilized();

            if (this.host.State == CommunicationState.Opened || this.host.State == CommunicationState.Opening)
            {
                return;
            }

            this.host.Open();
        }

        public void Stop()
        {
            if (this.host == null)
            {
                return;
            }

            if (this.host.State == CommunicationState.Closed || this.host.State == CommunicationState.Closing)
            {
                return;
            }

            this.host.Close();
        }

        public void Dispose()
        {
            // Stop running things
            this.Stop();

            // Unbind events
            this.service.RequestReceived -= this.OnRequestReceived;
            this.service.EchoReceived -= this.OnEchoReceived;

            // Remove references
            this.host = null;
        }

        #region Events

        public event RenderingRequestHandler RequestReceived;

        public event RenderingRequestHandler EchoReceived;

        #endregion

        private void EnsureInitilized()
        {
            if (this.host != null)
            {
                return;
            }

            this.service = new CommunicationService();
            this.service.RequestReceived += this.OnRequestReceived;
            this.service.EchoReceived += this.OnEchoReceived;

            this.host = new WebServiceHost(service, new Uri(this.Address));
            this.host.AddServiceEndpoint(typeof(RenderingApi.ICommunicationService), new WebHttpBinding(), "");
        }

        private string Address
        {
            get { return $"http://localhost:{this.port}/"; }
        }

        #region Event handlers

        private void OnRequestReceived(RenderingRequest request)
        {
            this.RequestReceived?.Invoke(request); // Redirect up above
        }

        private void OnEchoReceived(RenderingRequest request)
        {
            this.RequestReceived?.Invoke(request); // Redirect up above
        }

        #endregion
    }
}
