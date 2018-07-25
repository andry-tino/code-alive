/// <summary>
/// Andrea Tino - 2018
/// </summary>

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

/// <summary>
/// Provides functionalities to instantiate connectors.
/// </summary>
public class ConnectorInstantiator : MonoBehaviour
{
    #region Parameters

    /// <summary>
    /// The object to clone.
    /// </summary>
    public Transform Object;

    #endregion

    // Request handling
    private InstantiationRequest requested = null;

    private List<Transform> instances = new List<Transform>(); // Instantiated objects

    void Start () {
	}
	
	void Update () {
        if (this.requested != null)
        {
            var srcObj = GameObject.Find($"/{this.requested.Src}");
            var dstObj = GameObject.Find($"/{this.requested.Dst}");

            if (srcObj == null)
            {
                Debug.LogError($"[RenderMessageExchange] Could not find src object with id `{this.requested.Src}`");
                return;
            }

            if (dstObj == null)
            {
                Debug.LogError($"[RenderMessageExchange] Could not find dst object with id `{this.requested.Dst}`");
                return;
            }

            Vector3 start = srcObj.transform.position;
            Vector3 end = dstObj.transform.position;

            float width = 0.2f;
            Vector3 offset = end - start;
            Vector3 scale = new Vector3(width, offset.magnitude / 2f, width);
            Vector3 position = start + (offset / 2f);

            Transform copy = Instantiate(this.Object, position, Quaternion.identity);

            // Assign a name which will reflect the connector's nature
            copy.name = $"{this.requested.Src}__{this.requested.Dst}";

            copy.transform.up = offset;
            copy.transform.localScale = scale;

            // Add to cache
            this.instances.Add(copy);

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
