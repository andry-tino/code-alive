using System;
using System.Runtime.InteropServices;

using UnityEngine;

/// <summary>
/// Provides functionalities for building a mesh.
/// </summary>
public abstract class MeshBuilder
{
    /// <summary>
    /// Creates a new instance of the <see cref="MeshBuilder"/> class.
    /// </summary>
    /// <param name="verticesNumber"></param>
    public MeshBuilder()
    {
    }

    /// <summary>
    /// The vertices.
    /// </summary>
    protected Vector3[] Vertices { get; set; }

    /// <summary>
    /// The indices for triangles.
    /// </summary>
    protected int[] Triangles { get; set; }

    /// <summary>
    /// Creates the mesh.
    /// </summary>
    /// <returns></returns>
    public abstract Mesh Create();
}

/// <summary>
/// Responsible for creating a mesh for a cell.
/// </summary>
public class StaticCellMeshBuilder : MeshBuilder
{
    /// <summary>
    /// Creates a new instance of the <see cref="StaticCellMeshBuilder"/> class.
    /// </summary>
    public StaticCellMeshBuilder() : base()
    {
    }

    public override Mesh Create()
    {
        this.Vertices = new Vector3[] 
        {
            new Vector3(0, 0, 0),
            new Vector3(0, 2, 0),
            new Vector3(1, 1, 1),
            new Vector3(2, 0, 0),
            new Vector3(2, 2, 0)
        };

        this.Triangles = new int[] 
        {
            4, 1, 2,
            4, 2, 3,
            1, 4, 3,
            1, 0, 2,
            3, 0, 1,
            2, 0, 3
        };

        var mesh = new Mesh();
        mesh.vertices = this.Vertices;
        mesh.triangles = this.Triangles;

        return mesh;
    }
}

/// <summary>
/// Responsible for creating a mesh for a cell.
/// </summary>
public class CellMeshBuilder : MeshBuilder
{
    /// <summary>
    /// Creates a new instance of the <see cref="CellMeshBuilder"/> class.
    /// </summary>
    /// <param name="verticesNumber"></param>
    public CellMeshBuilder(int verticesNumber = 10) : base()
    {
    }

    public override Mesh Create()
    {
        throw new NotImplementedException();
    }
}
