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
        [WebGet]
        string Echo(string message);

        /// <summary>
        /// An echo service message.
        /// </summary>
        /// <param name="message">Message to be echoed mack by the server.</param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke]
        string Echo_Post(string message);
    }
}
