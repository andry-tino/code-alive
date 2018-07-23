/// <summary>
/// Andrea Tino - 2018
/// </summary>

namespace CodeAlive.Communication.RenderingApi
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Class responsible for describing the request for rendering an interaction between two instances (cells).
    /// </summary>
    [DataContract(Name = "InteractionRenderingRequest")]
    public class InteractionRenderingRequest : RenderingRequest
    {
        [DataMember(Name = "InvocationName", IsRequired = false)]
        public string InvocationName { get; set; }

        [DataMember(Name = "SourceInstanceId", IsRequired = true)]
        public string SourceInstanceId { get; set; }

        [DataMember(Name = "DstInstanceId", IsRequired = true)]
        public string DstInstanceId { get; set; }
    }
}
