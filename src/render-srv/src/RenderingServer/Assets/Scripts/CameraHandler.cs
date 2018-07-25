/// <summary>
/// Andrea Tino - 2018
/// </summary>

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    #region Parameters

    /// <summary>
    /// The point to look at while revolving around.
    /// </summary>
    public Vector3 RevolutionPoint = new Vector3(0, 0, 0);

    /// <summary>
    /// The revolution speed.
    /// </summary>
    public float Speed = 10f;

    #endregion

    private Transform cameraObject = null;

    void Start() {
        // Cache it
        this.cameraObject = GetComponent<Transform>();

        this.cameraObject.LookAt(this.RevolutionPoint); // Makes the camera look to it
    }
	
	void Update() {
        this.cameraObject.RotateAround(this.RevolutionPoint, new Vector3(0.0f, 1.0f, 0.0f), 20 * Time.deltaTime * this.Speed);
    }
}
