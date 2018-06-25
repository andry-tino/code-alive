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

    private ServiceHost host;

    public CommunicationEndpoint()
    {
    }

    public void Start()
    {
        this.EnsureInitilized();

        if (this.host.State == CommunicationState.Opened || this.host.State == CommunicationState.Opening)
        {
            return;
        }

        this.host.Open();
    }

    public void Stop()
    {
        if (this.host == null)
        {
            return;
        }

        if (this.host.State == CommunicationState.Closed || this.host.State == CommunicationState.Closing)
        {
            return;
        }

        this.host.Close();
    }

    public void Dispose()
    {
        this.Stop();

        this.host = null;
    }

    private void EnsureInitilized()
    {
        if (this.host != null)
        {
            return;
        }

        this.host = new WebServiceHost(typeof(CommunicationService), new Uri(this.Address));
        this.host.AddServiceEndpoint(typeof(ICommunicationService), new WebHttpBinding(), "");
    }

    private string Address
    {
        get { return $"http://localhost:{Port}/"; }
    }
}
