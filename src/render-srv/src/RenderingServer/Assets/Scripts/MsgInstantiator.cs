/// <summary>
/// Andrea Tino - 2018
/// </summary>

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

/// <summary>
/// Instantiates messages.
/// </summary>
public class MsgInstantiator : MonoBehaviour
{
    #region Parameters

    /// <summary>
    /// The object to clone.
    /// </summary>
    public Msg Object;

    #endregion

    // Request handling
    private InstantiationRequest requested = null;

    void Start() {	
	}
	
	void Update() {
        if (this.requested != null)
        {
            var srcObj = GameObject.Find($"/{this.requested.Src}");
            var dstObj = GameObject.Find($"/{this.requested.Dst}");

            if (srcObj == null)
            {
                Debug.LogError($"[MsgInstantiator] Could not find src object with id `{this.requested.Src}`");
                return;
            }

            if (dstObj == null)
            {
                Debug.LogError($"[MsgInstantiator] Could not find dst object with id `{this.requested.Dst}`");
                return;
            }

            Vector3 start = srcObj.transform.position;
            Vector3 end = dstObj.transform.position;

            Msg copy = Instantiate<Msg>(this.Object, start, Quaternion.identity);
            copy.StartPoint = start;
            copy.EndPoint = end;
            copy.Duration = 2f; // In seconds

            // Reset
            this.requested = null;
        }
    }

    public void CreateNew(string name, string srcId, string dstId)
    {
        this.requested = new InstantiationRequest()
        {
            Name = name,
            Src = srcId,
            Dst = dstId
        };
    }

    #region Types

    private class InstantiationRequest
    {
        public string Name { get; set; }
        public string Src { get; set; }
        public string Dst { get; set; }
    }

    #endregion
}
