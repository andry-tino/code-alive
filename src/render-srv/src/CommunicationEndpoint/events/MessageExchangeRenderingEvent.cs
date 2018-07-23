/// <summary>
/// Andrea Tino - 2018
/// </summary>

namespace CodeAlive.Communication.Eventing
{
    using System;

    /// <summary>
    /// Event descriptor.
    /// </summary>
    public class MessageExchangeRenderingEvent : RenderingEvent
    {
        /// <summary>
        /// The identifier of the new instance.
        /// </summary>
        public string InvocationName { get; set; }

        /// <summary>
        /// The identifier of the instance marked here as `source`.
        /// </summary>
        public string SourceId { get; set; }

        /// <summary>
        /// The identifier of the instance marked here as `destination`.
        /// </summary>
        public string DestinationId { get; set; }

        /// <summary>
        /// Provides a string representation.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "MessageExchangeRenderingEvent";
        }
    }
}
