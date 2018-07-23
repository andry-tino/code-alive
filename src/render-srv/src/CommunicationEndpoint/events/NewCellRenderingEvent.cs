/// <summary>
/// Andrea Tino - 2018
/// </summary>

namespace CodeAlive.Communication.Eventing
{
    using System;

    /// <summary>
    /// Event descriptor.
    /// </summary>
    public class NewCellRenderingEvent : RenderingEvent
    {
        /// <summary>
        /// The identifier of the new instance.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Provides a string representation.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "NewInstanceRenderingEvent";
        }
    }
}
