using System;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace CommunicationClient
{
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
