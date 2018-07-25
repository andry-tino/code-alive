/// <summary>
/// Andrea Tino - 2018
/// </summary>

namespace CodeAlive.Communication
{
    using System;
    using System.ServiceModel;

    using CodeAlive.Communication.RenderingApi;
    
    internal delegate void NewInstanceRenderingRequestHandler(NewInstanceRenderingRequest request);
    internal delegate void InteractionRenderingRequestHandler(InteractionRenderingRequest request);
    internal delegate void GetReferenceRenderingRequestHandler(GetReferenceRenderingRequest request);
    internal delegate string DiagnosticRenderingRequestHandler(DiagnosticRenderingRequest request);

    /// <summary>
    /// The implementation.
    /// </summary> 
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    internal class CommunicationService : ICommunicationService
    {
        public string Echo(DiagnosticRenderingRequest message)
        {
            return this.DiagnosticReceived?.Invoke(message);
        }

        public void RenderNewCell(NewInstanceRenderingRequest message)
        {
            this.NewInstanceReceived?.Invoke(message);
        }

        public void RenderInteraction(InteractionRenderingRequest message)
        {
            this.InteractionReceived?.Invoke(message);
        }

        public void RenderReference(GetReferenceRenderingRequest message)
        {
            this.NewReferenceReceived?.Invoke(message);
        }

        #region Events

        /// <summary>
        /// Fired when a diagnostic is received.
        /// </summary>
        public event DiagnosticRenderingRequestHandler DiagnosticReceived;

        /// <summary>
        /// Fired when a new instance is requested to be rendered.
        /// </summary>
        public event NewInstanceRenderingRequestHandler NewInstanceReceived;

        /// <summary>
        /// Fired when an interaction is requested to be rendered.
        /// </summary>
        public event InteractionRenderingRequestHandler InteractionReceived;

        /// <summary>
        /// Fired when a new reference is requested to be rendered.
        /// </summary>
        public event GetReferenceRenderingRequestHandler NewReferenceReceived;

        #endregion
    }
}
