/// <summary>
/// Andrea Tino - 2018
/// </summary>

namespace CodeAlive.Communication
{
    using System;
    using System.ServiceModel;
    using System.ServiceModel.Web;

    using CodeAlive.Communication.RenderingApi;

    /// <summary>
    /// Holds the endpoint logic.
    /// </summary>
    internal class CommunicationEndpoint : IDisposable
    {
        public const short DefaultPort = 8000;
        public const string DefaultHostName = "localhost";

        private string hostname;
        private short port;
        private CommunicationService service;
        private ServiceHost host;

        /// <summary>
        /// Creates a new instance of the <see cref="CommunicationEndpoint"/> class.
        /// </summary>
        /// <param name="hostname">The hostname to use. Defaults to <see cref="DefaultHostName"/>.</param>
        /// <param name="port">The port to use. Defaults to <see cref="DefaultPort"/>.</param>
        public CommunicationEndpoint(string hostname = DefaultHostName, short port = DefaultPort)
        {
            this.hostname = hostname;
            this.port = port;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="CommunicationEndpoint"/> class.
        /// </summary>
        /// <param name="port">The port to use.</param>
        public CommunicationEndpoint(short port) : this(DefaultHostName, port)
        {
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
            this.service.DiagnosticReceived -= this.OnDiagnosticReceived;
            this.service.NewInstanceReceived -= this.OnNewInstanceReceived;
            this.service.InteractionReceived -= this.OnInteractionReceived;
            this.service.NewReferenceReceived -= this.OnNewReferenceReceived;

            // Remove references
            this.host = null;
        }

        #region Events
        
        public event DiagnosticRenderingRequestHandler DiagnosticReceived;
        
        public event NewInstanceRenderingRequestHandler NewInstanceReceived;
        
        public event InteractionRenderingRequestHandler InteractionReceived;

        public event GetReferenceRenderingRequestHandler NewReferenceReceived;

        #endregion

        private void EnsureInitilized()
        {
            if (this.host != null)
            {
                return;
            }

            this.service = new CommunicationService();
            this.service.DiagnosticReceived += this.OnDiagnosticReceived;
            this.service.NewInstanceReceived += this.OnNewInstanceReceived;
            this.service.InteractionReceived += this.OnInteractionReceived;
            this.service.NewReferenceReceived += this.OnNewReferenceReceived;

            this.host = new WebServiceHost(service, new Uri(this.Address));
            this.host.AddServiceEndpoint(typeof(RenderingApi.ICommunicationService), new WebHttpBinding(), "");
        }

        private string Address
        {
            get { return $"http://{this.hostname}:{this.port}/"; }
        }

        #region Event handlers

        private string OnDiagnosticReceived(DiagnosticRenderingRequest request)
        {
            return this.DiagnosticReceived?.Invoke(request); // Redirect up above
        }

        private void OnNewInstanceReceived(NewInstanceRenderingRequest request)
        {
            this.NewInstanceReceived?.Invoke(request); // Redirect up above
        }

        private void OnInteractionReceived(InteractionRenderingRequest request)
        {
            this.InteractionReceived?.Invoke(request); // Redirect up above
        }

        private void OnNewReferenceReceived(GetReferenceRenderingRequest request)
        {
            this.NewReferenceReceived?.Invoke(request); // Redirect up above
        }

        #endregion
    }
}
