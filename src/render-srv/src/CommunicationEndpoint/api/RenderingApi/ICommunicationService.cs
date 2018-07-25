/// <summary>
/// Andrea Tino - 2018
/// </summary>

namespace CodeAlive.Communication.RenderingApi
{
    using System;
    using System.ServiceModel;
    using System.ServiceModel.Web;

    /// <summary>
    /// The contract for service.
    /// </summary>
    /// <remarks>
    /// Passed values are passed from client. Return values are server responses.
    /// </remarks>
    [ServiceContract]
    public interface ICommunicationService
    {
        /// <summary>
        /// An echo service message.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke]
        string Echo(DiagnosticRenderingRequest message);

        /// <summary>
        /// Ask to render a new cell.
        /// </summary>
        [OperationContract]
        [WebInvoke]
        void RenderNewCell(NewInstanceRenderingRequest message);

        /// <summary>
        /// Ask to render an interaction between two cells.
        /// </summary>
        [OperationContract]
        [WebInvoke]
        void RenderInteraction(InteractionRenderingRequest message);

        /// <summary>
        /// Ask to render an aquired reference relation.
        /// </summary>
        [OperationContract]
        [WebInvoke]
        void RenderReference(GetReferenceRenderingRequest message);
    }
}
