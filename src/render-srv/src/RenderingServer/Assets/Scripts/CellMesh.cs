﻿using System;
using System.Runtime.InteropServices;

using UnityEngine;

/// <summary>
/// Provides functionalities for building a mesh.
/// </summary>
public class CellMesh : MonoBehaviour
{
    void Start()
    {
        var vertices = new Vector3[]
        {
            new Vector3(0, 0, 0),
            new Vector3(0, 2, 0),
            new Vector3(1, 1, 1),
            new Vector3(2, 0, 0),
            new Vector3(2, 2, 0)
        };

        var triangles = new int[]
        {
            4, 1, 2,
            4, 2, 3,
            1, 4, 3,
            1, 0, 2,
            3, 0, 1,
            2, 0, 3
        };

        var mesh = GetComponent<MeshFilter>().mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
    }
}
