/// <summary>
/// Andrea Tino - 2018
/// </summary>

namespace CodeAlive.Communication.Eventing
{
    using System;

    /// <summary>
    /// Event descriptor.
    /// </summary>
    public class NewReferenceRenderingEvent : RenderingEvent
    {
        /// <summary>
        /// The identifier of the instance marked here as the one getting owned.
        /// </summary>
        public string InstanceId { get; set; }

        /// <summary>
        /// The identifier of the instance marked here as the one acquiring ownership.
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// Provides a string representation.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "NewReferenceRenderingEvent";
        }
    }
}
