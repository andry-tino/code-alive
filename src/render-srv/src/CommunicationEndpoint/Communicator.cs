///
/// Andrea Tino - 2018
///

namespace CodeAlive.Communication
{
    using System;

    using CodeAlive.Communication.Eventing;

    public delegate void RenderingEventHandler(RenderingEvent e);
    public delegate void DiagnosticRenderingEventHandler(DiagnosticRenderingEvent e);
    public delegate void EchoRenderingEventHandler(EchoRenderingEvent e);

    /// <summary>
    /// The interface for interacting with the Communication API.
    /// This object is used to listen to requests. Subscribe to its events in order to receive notifications about rendering events.
    /// </summary>
    public class Communicator : IDisposable
    {
        public const short DefaultPort = 8000;

        private CommunicationEndpoint svc;
        
        /// <summary>
        /// Creates a new instance of the <see cref="Communicator"/> class.
        /// </summary>
        /// <param name="port">The port to use.</param>
        public Communicator(short port = DefaultPort)
        {
            this.svc = new CommunicationEndpoint(port);
        }

        public void Initialize()
        {
            this.svc.RequestReceived += this.OnRequestReceived;
            this.svc.EchoReceived += this.OnEchoReceived;

            this.svc.Start(); // Start the service
        }

        public void Dispose()
        {
            // Unbind events
            this.svc.RequestReceived -= this.OnRequestReceived;
            this.svc.EchoReceived -= this.OnEchoReceived;

            // Dispose stuff
            this.svc.Dispose();
        }

        #region Events

        /// <summary>
        /// Fired every time an events occurred. This event is always fired.
        /// </summary>
        public event RenderingEventHandler EventOccurred;

        /// <summary>
        /// Fired when an echo is received.
        /// </summary>
        public event EchoRenderingEventHandler EchoOccurred;

        /// <summary>
        /// Fired when a diagnostic event is received.
        /// </summary>
        public event DiagnosticRenderingEventHandler DiagnosticOccurred;

        #endregion

        #region Lower event handlers

        private void OnRequestReceived(RenderingRequest request)
        {
            var e = new DiagnosticRenderingEvent();
            e.Message = "Request received (generic)";

            this.EventOccurred?.Invoke(e);
        }

        private void OnEchoReceived(RenderingRequest request)
        {
            var e = new EchoRenderingEvent();
            e.Content = "Echo received";

            this.EchoOccurred?.Invoke(e);
        }

        #endregion
    }
}
