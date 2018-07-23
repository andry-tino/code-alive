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
        public string EchoWithGet(string s)
        {
            this.RequestReceived(new RenderingRequest());
            return "You said " + s;
        }

        public string EchoWithPost(string s)
        {
            this.RequestReceived(new RenderingRequest());
            return "You said " + s;
        }

        /// <summary>
        /// Fired when a request is received from the client.
        /// </summary>
        public event RenderingRequestHandler RequestReceived;
    }
}
