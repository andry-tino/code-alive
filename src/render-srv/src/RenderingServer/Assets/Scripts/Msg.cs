/// <summary>
/// Andrea Tino - 2018
/// </summary>

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

/// <summary>
/// Describes a message in an exchange and handles its animation.
/// </summary>
public class Msg : MonoBehaviour
{
    #region Parameters

    /// <summary>
    /// The start point.
    /// </summary>
    public Vector3 StartPoint;

    /// <summary>
    /// The end point.
    /// </summary>
    public Vector3 EndPoint;

    /// <summary>
    /// Animation duration in seconds.
    /// </summary>
    public float Duration = 2.0f;

    #endregion

    private float startTime = 0;

    void Start() {
        this.transform.position = this.StartPoint;

        this.startTime = Time.time;
	}
	
	void Update() {
        float t = this.TimeCursor;
        if (t > 1)
        {
            // Self destruct as animation is over
            Destroy(this.gameObject);
        }

        this.transform.position = this.EndPoint * t + ((1 - t) * this.StartPoint);
	}

    private float TimeCursor => (Time.time - this.startTime) / this.Duration;
}
