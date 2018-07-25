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
    [DataContract(Name = "GetReferenceRenderingRequest")]
    public class GetReferenceRenderingRequest : RenderingRequest
    {
        [DataMember(Name = "InstanceId", IsRequired = true)]
        public string InstanceId { get; set; }

        [DataMember(Name = "ParentInstanceId", IsRequired = true)]
        public string ParentInstanceId { get; set; }
    }
}
