/// <summary>
/// Andrea Tino - 2018
/// </summary>

using System;
using System.Runtime.InteropServices;

using UnityEngine;

/// <summary>
/// Provides functionalities for building a mesh.
/// </summary>
public class CellMesh : MonoBehaviour
{
    #region Declarations for interop

    private const string DllPath = @"C:\Users\antino\Documents\GitHub\code-alive\src\triangulator\src\DelaunayTriangulator\Debug\DelaunayTriangulator.dll";

    [DllImport(DllPath)]
    private static extern void codealive_triangulator_perform(int num);

    [DllImport(DllPath)]
    private static extern void codealive_triangulator_dispose();

    [DllImport(DllPath)]
    private static extern int codealive_triangulator_get_vertices_num();

    [DllImport(DllPath)]
    private static extern int codealive_triangulator_get_triangles_vlen();

    [DllImport(DllPath)]
    private static extern double codealive_triangulator_get_vertex(int index, int vindex);

    [DllImport(DllPath)]
    private static extern IntPtr codealive_triangulator_get_triangles();

    #endregion

    void Start()
    {
        //this.CreateBasicVoxel();
        this.CreateFromTriangulation(30);
    }

    void CreateFromTriangulation(int numberOfPoints)
    {
        // Generate rnd set and triangulate
        codealive_triangulator_perform(numberOfPoints);

        // Handle vertices
        int numberOfVertices = codealive_triangulator_get_vertices_num();

        Vector3[] vertices = new Vector3[numberOfVertices];
        for (int i = 0; i < numberOfVertices; i++)
        {
            double x = codealive_triangulator_get_vertex(i, 0);
            double y = codealive_triangulator_get_vertex(i, 1);
            double z = codealive_triangulator_get_vertex(i, 2);

            vertices[i] = new Vector3((float)x, (float)y, (float)z);
        }

        // Handle triangles
        int trianglesLen = codealive_triangulator_get_triangles_vlen();
        IntPtr trianglesPtr = codealive_triangulator_get_triangles();
        int[] triangles = ExtractArray(trianglesPtr, trianglesLen);

        // Clear up memory
        codealive_triangulator_dispose();

        // Create vector of normals
        Vector3[] normals = new Vector3[numberOfVertices];
        for (int i = 0; i < numberOfVertices; i++)
        {
            normals[i] = -Vector3.forward;
        }

        // Build mesh
        var mesh = GetComponent<MeshFilter>().mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.normals = normals;
    }

    void CreateBasicVoxel()
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

        var normals = new Vector3[]
        {
            -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward
        };

        var mesh = GetComponent<MeshFilter>().mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.normals = normals;
    }

    void Update()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;
        Vector3[] normals = mesh.normals;

        System.Random rnd = new System.Random();

        for (int i = 0, l = vertices.Length; i < l; i++)
        {
            vertices[i] += normals[i] * 0.002f * rnd.Next(0, 10) * Mathf.Sin(Time.time);
        }

        mesh.vertices = vertices;
    }

    private static int[] ExtractArray(IntPtr ptr, int arrayLength)
    {
        int[] result = new int[arrayLength];
        Marshal.Copy(ptr, result, 0, arrayLength);

        return result;
    }
}
