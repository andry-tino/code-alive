///
/// Andrea Tino - 2018
///

namespace CodeAlive.Communication
{
    using System;

    using CodeAlive.Communication.Eventing;
    using CodeAlive.Communication.RenderingApi;

    public delegate void DiagnosticRenderingEventHandler(DiagnosticRenderingEvent e);
    public delegate void NewCellRenderingEventHandler(NewCellRenderingEvent e);
    public delegate void MessageExchangeRenderingEventHandler(MessageExchangeRenderingEvent e);

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
            this.svc.DiagnosticReceived += this.OnDiagnosticReceived;

            this.svc.Start(); // Start the service
        }

        public void Dispose()
        {
            // Unbind events
            this.svc.DiagnosticReceived -= this.OnDiagnosticReceived;

            // Dispose stuff
            this.svc.Dispose();
        }

        #region Events
        

        /// <summary>
        /// Fired when a diagnostic event is received.
        /// </summary>
        public event DiagnosticRenderingEventHandler DiagnosticOccurred;

        /// <summary>
        /// Fired when a new cell should be drawn.
        /// </summary>
        public event NewCellRenderingEventHandler NewCellOccurred;

        /// <summary>
        /// Fired when an exchange should be drawn.
        /// </summary>
        public event MessageExchangeRenderingEventHandler MessageExchangeOccurred;

        #endregion

        #region Lower event handlers

        private string OnDiagnosticReceived(DiagnosticRenderingRequest request)
        {
            var e = new DiagnosticRenderingEvent();
            e.Content = request.Content;

            this.DiagnosticOccurred?.Invoke(e);

            return request.Content.Clone() as string;
        }

        private void OnNewInstanceReceived(NewInstanceRenderingRequest request)
        {
            var e = new NewCellRenderingEvent();
            e.Id = request.InstanceId;

            this.NewCellOccurred?.Invoke(e);
        }

        private void OnInteractionReceived(InteractionRenderingRequest request)
        {
            var e = new MessageExchangeRenderingEvent();
            e.InvocationName = request.InvocationName;
            e.SourceId = request.SourceInstanceId;
            e.DestinationId = request.DstInstanceId;

            this.MessageExchangeOccurred?.Invoke(e);
        }

        #endregion
    }
}
