/// <summary>
/// Andrea Tino - 2018
/// </summary>

namespace CodeAlive.Communication.RenderingApi
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Class responsible for describing the request for a ping.
    /// </summary>
    [DataContract(Name = "EchoRenderingRequest")]
    public class EchoRenderingRequest : RenderingRequest
    {
        [DataMember(Name = "Content", IsRequired = true)]
        public string Content { get; set; }
    }
}
