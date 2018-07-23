///
/// Andrea Tino - 2018
///

namespace CodeAlive.Communication
{
    using System;

    internal delegate void RenderingRequestHandler(RenderingRequest request);

    /// <summary>
    /// The implementation.
    /// </summary>
    internal class CommunicationService : ICommunicationService
    {
        public string EchoWithGet(string s)
        {
            return "You said " + s;
        }

        public string EchoWithPost(string s)
        {
            return "You said " + s;
        }

        /// <summary>
        /// Fired when a request is received from the client.
        /// </summary>
        public event RenderingRequestHandler RequestReceived;
    }
}
