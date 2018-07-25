/// <summary>
/// Andrea Tino - 2018
/// </summary>

namespace CodeAlive.Communication.RenderingApi
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Class responsible for describing the request for rendering a new instance.
    /// </summary>
    [DataContract(Name = "NewInstanceRenderingRequest")]
    public class NewInstanceRenderingRequest : RenderingRequest
    {
        [DataMember(Name = "ParentInstanceId", IsRequired = false)]
        public string ParentInstanceId { get; set; }

        [DataMember(Name = "InstanceId", IsRequired = true)]
        public string InstanceId { get; set; }
    }
}
