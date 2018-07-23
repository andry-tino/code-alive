/// <summary>
/// Andrea Tino - 2018
/// </summary>

namespace CodeAlive.Communication.RenderingApi
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Class responsible for describing the request for a diagnostic message display.
    /// </summary>
    [DataContract(Name = "DiagnosticRenderingRequest")]
    public class DiagnosticRenderingRequest : EchoRenderingRequest
    {
    }
}
