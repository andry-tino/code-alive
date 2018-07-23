///
/// Andrea Tino - 2018
///

namespace CodeAlive.Communication
{
    using System;

    using CodeAlive.Communication.Eventing;

    public delegate void RenderingEventHandler(RenderingEvent e);

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
            this.svc.Start(); // Start the service
        }

        /// <summary>
        /// Fired every time an events occurred. This event is always fired.
        /// </summary>
        public event RenderingEventHandler EventOccurred;
        
        private void OnRequestReceived(RenderingRequest request)
        {
            this.TriggerEvent(request);
        }

        /// <summary>
        /// Basing on the request, it triggers the correct event.
        /// </summary>
        /// <param name="request"></param>
        private void TriggerEvent(RenderingRequest request)
        {   
            this.EventOccurred(new RenderingEvent());
        }

        public void Dispose()
        {
            // Unbind events
            this.svc.RequestReceived -= this.OnRequestReceived;

            // Dispose stuff
            this.svc.Dispose();
        }
    }
}
