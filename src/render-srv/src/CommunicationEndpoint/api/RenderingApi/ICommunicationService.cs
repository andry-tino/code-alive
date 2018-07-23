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
        /// <param name="message">Message to be echoed mack by the server.</param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke]
        string Echo(DiagnosticRenderingRequest message);

        /// <summary>
        /// Ask to render a new cell.
        /// </summary>
        /// <param name="cellId"></param>
        [OperationContract]
        [WebInvoke]
        void RenderNewCell(NewInstanceRenderingRequest message);

        /// <summary>
        /// Ask to render an interaction between two cells.
        /// </summary>
        /// <param name="interactionName"></param>
        /// <param name="sourceCellId"></param>
        /// <param name="dstCellId"></param>
        [OperationContract]
        [WebInvoke]
        void RenderInteraction(InteractionRenderingRequest message);
    }
}
