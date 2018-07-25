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

    private Vector3[] vertices; // Original vertices
    private System.Random rnd = new System.Random();

    private int updateCounter = 0;
    private int updateThreshold = 16;

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

        this.vertices = new Vector3[numberOfVertices];
        for (int i = 0; i < numberOfVertices; i++)
        {
            double x = codealive_triangulator_get_vertex(i, 0);
            double y = codealive_triangulator_get_vertex(i, 1);
            double z = codealive_triangulator_get_vertex(i, 2);

            this.vertices[i] = new Vector3((float)x, (float)y, (float)z);
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
        mesh.vertices = this.vertices;
        mesh.triangles = triangles;
        mesh.normals = normals;
    }

    void Update()
    {
        this.updateCounter = (this.updateCounter + 1) % this.updateThreshold;
        if (this.updateCounter != 0)
        {
            return;
        }

        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] newVertices = new Vector3[this.vertices.Length];
        
        for (int i = 0, l = vertices.Length; i < l; i++)
        {
            float spicex = (float)this.rnd.NextDouble() - 0.5f; // Distributed in [-.5, .5]
            if (spicex > 0)
            {
                // Don't spice this vertex => Skip it
                newVertices[i] = this.vertices[i];

                continue;
            }

            float spicey = (float)this.rnd.NextDouble() - 0.5f;
            float spicez = (float)this.rnd.NextDouble() - 0.5f;
            Vector3 rndAdditiveVector = 0.35f * new Vector3(spicex, spicey, spicez);

            newVertices[i] = this.vertices[i] + rndAdditiveVector;
        }

        mesh.vertices = newVertices;
    }

    private static int[] ExtractArray(IntPtr ptr, int arrayLength)
    {
        int[] result = new int[arrayLength];
        Marshal.Copy(ptr, result, 0, arrayLength);

        return result;
    }
}
