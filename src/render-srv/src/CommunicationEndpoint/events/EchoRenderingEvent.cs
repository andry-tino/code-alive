/// <summary>
/// Andrea Tino - 2018
/// </summary>

namespace CodeAlive.Communication.Eventing
{
    using System;

    /// <summary>
    /// Event descriptor.
    /// </summary>
    public class EchoRenderingEvent : RenderingEvent
    {
        /// <summary>
        /// The echo content.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Provides a string representation.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "EchoRenderingEvent";
        }
    }
}
