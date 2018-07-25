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
    public delegate void NewReferenceRenderingEventHandler(NewReferenceRenderingEvent e);

    /// <summary>
    /// The interface for interacting with the Communication API.
    /// This object is used to listen to requests. Subscribe to its events in order to receive notifications about rendering events.
    /// </summary>
    public class Communicator : IDisposable
    {
        public const short DefaultPort = 8000;
        public const string DefaultHostName = "localhost";

        private CommunicationEndpoint svc;
        
        /// <summary>
        /// Creates a new instance of the <see cref="Communicator"/> class.
        /// </summary>
        /// <param name="hostname">The hostname to use.</param>
        /// <param name="port">The port to use.</param>
        public Communicator(string hostname = DefaultHostName, short port = DefaultPort)
        {
            this.svc = new CommunicationEndpoint(hostname, port);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Communicator"/> class.
        /// </summary>
        /// <param name="port">The port to use.</param>
        public Communicator(short port) : this(DefaultHostName, port)
        {
        }

        public void Initialize()
        {
            this.svc.DiagnosticReceived += this.OnDiagnosticReceived;
            this.svc.NewInstanceReceived += this.OnNewInstanceReceived;
            this.svc.InteractionReceived += this.OnInteractionReceived;
            this.svc.NewReferenceReceived += this.OnNewReferenceReceived;

            this.svc.Start(); // Start the service
        }

        public void Dispose()
        {
            // Unbind events
            this.svc.DiagnosticReceived -= this.OnDiagnosticReceived;
            this.svc.NewInstanceReceived -= this.OnNewInstanceReceived;
            this.svc.InteractionReceived -= this.OnInteractionReceived;
            this.svc.NewReferenceReceived -= this.OnNewReferenceReceived;

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

        /// <summary>
        /// Fired when a new reference should be drawn.
        /// </summary>
        public event NewReferenceRenderingEventHandler NewReferenceOccurred;

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

        private void OnNewReferenceReceived(GetReferenceRenderingRequest request)
        {
            var e = new NewReferenceRenderingEvent();
            e.InstanceId = request.InstanceId;
            e.ParentId = request.ParentInstanceId;

            this.NewReferenceOccurred?.Invoke(e);
        }

        #endregion
    }
}
