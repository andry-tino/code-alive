/// <summary>
/// Andrea Tino - 2018
/// </summary>

namespace CodeAlive.Communication
{
    using System;
    using System.ServiceModel;

    internal delegate void RenderingRequestHandler(RenderingRequest request);

    /// <summary>
    /// The implementation.
    /// </summary> 
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    internal class CommunicationService : RenderingApi.ICommunicationService
    {
        public string Echo(string message)
        {
            this.RaiseDefaultEvents();
            this.RaiseEcho();

            // Return the exact same message
            return message;
        }

        #region Post alternatives

        public string Echo_Post(string message)
        {
            return this.Echo(message);
        }

        #endregion

        #region Events

        /// <summary>
        /// Fired when a request is received from the client.
        /// </summary>
        public event RenderingRequestHandler RequestReceived;

        /// <summary>
        /// Fired when an echo is received.
        /// </summary>
        public event RenderingRequestHandler EchoReceived;

        #endregion

        #region Utils

        private void RaiseEcho()
        {
            this.EchoReceived?.Invoke(new RenderingRequest());
        }

        private void RaiseDefaultEvents()
        {
            this.RequestReceived?.Invoke(new RenderingRequest());
        }
        #endregion
    }
}
