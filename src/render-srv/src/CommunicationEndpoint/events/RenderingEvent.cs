/// <summary>
/// Andrea Tino - 2018
/// </summary>

namespace CodeAlive.Communication.Eventing
{
    using System;

    /// <summary>
    /// Event descriptor.
    /// </summary>
    public class RenderingEvent
    {
        /// <summary>
        /// The type of event.
        /// </summary>
        public RenderingEventType Type { get; set; }

        /// <summary>
        /// When specified, the debug console will display a message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Provides a string representation.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "RenderingEvent";
        }
    }

    /// <summary>
    /// The type of event.
    /// </summary>
    public enum RenderingEventType
    {
        /// <summary>
        /// Nothing to render, just a diagnostic event.
        /// </summary>
        Diagnostic = 0,

        /// <summary>
        /// A molecule is created.
        /// </summary>
        InstanceCreated = 1,

        /// <summary>
        /// Interaction between molecules: message passing.
        /// </summary>
        MessagePassing = 2
    }
}
