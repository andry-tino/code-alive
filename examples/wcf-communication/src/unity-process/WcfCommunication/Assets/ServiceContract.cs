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

/// <summary>
/// The implementation.
/// </summary>
public class CommunicationService : ICommunicationService
{
    public string EchoWithGet(string s)
    {
        return "You said " + s;
    }

    public string EchoWithPost(string s)
    {
        return "You said " + s;
    }
}
