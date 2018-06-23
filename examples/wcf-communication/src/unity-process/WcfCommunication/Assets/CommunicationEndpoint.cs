using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Web;

/// <summary>
/// Holds the endpoint logic.
/// </summary>
public class CommunicationEndpoint : IDisposable
{
    private const short Port = 8000;

    private WebServiceHost host;
    private ServiceEndpoint endpoint;

    public CommunicationEndpoint()
    {
    }

    public void Start()
    {
        this.EnsureInitilized();

        host.Open();
    }

    public void Stop()
    {
        if (this.host != null)
        {
            return;
        }

        host.Close();
    }

    public void Dispose()
    {
        if (this.host.State != CommunicationState.Closed)
        {
            this.host.Close();
        }

        this.host = null;
    }

    private void EnsureInitilized()
    {
        if (this.host != null)
        {
            return;
        }

        this.host = new WebServiceHost(typeof(CommunicationService), new Uri(this.Address));
        this.endpoint = this.host.AddServiceEndpoint(typeof(ICommunicationService), new WebHttpBinding(), "");
    }

    private string Address
    {
        get { return "http://localhost:" + Port + "/"; }
    }
}
