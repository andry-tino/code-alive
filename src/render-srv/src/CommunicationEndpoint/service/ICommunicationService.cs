///
/// Andrea Tino - 2018
///

namespace CodeAlive.Communication
{
    using System;
    using System.ServiceModel;
    using System.ServiceModel.Web;

    /// <summary>
    /// The contract.
    /// </summary>
    [ServiceContract]
    public interface ICommunicationService
    {
        [OperationContract]
        [WebGet]
        string EchoWithGet(string s);

        [OperationContract]
        [WebInvoke]
        string EchoWithPost(string s);
    }
}
