using System.Collections;
using System.Collections.Generic;
using System.Linq;

using NTS = NetTopologySuite;
using Geo = GeoAPI;

using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class MeshGenerator : MonoBehaviour {
    private Vector3[] vertices;
    private int[] triangles;

    private NTS.Geometries.MultiPoint pyramid;
    private Dictionary<string, int> dict;

    private Mesh mesh;

    // Use this for initialization
    void Start()
    {
        this.Initialize();
        this.Triangulation();

        this.mesh.vertices = this.vertices;
        this.mesh.triangles = this.triangles;
    }

    void Awake()
    {
        this.mesh = GetComponent<MeshFilter>().mesh;
    }
	
	// Update is called once per frame
	void Update()
    {
        
	}

    private Geo.Geometries.IGeometry SquarePyramid
    {
        get
        {
            if (this.pyramid == null)
            {
                this.pyramid = new NTS.Geometries.MultiPoint(new[] {
                    new NTS.Geometries.Point(0, 0, 0),
                    new NTS.Geometries.Point(2, 0, 0),
                    new NTS.Geometries.Point(0, 2, 0),
                    new NTS.Geometries.Point(2, 2, 0),
                    new NTS.Geometries.Point(1, 1, 2)
                });

                this.dict = new Dictionary<string, int>();
                this.dict["0 0 0"] = 0;
                this.dict["2 0 0"] = 1;
                this.dict["0 2 0"] = 2;
                this.dict["2 2 0"] = 3;
                this.dict["1 1 2"] = 4;
            }

            return this.pyramid;
        }
    }

    private void Initialize()
    {
        this.vertices = null;
        this.triangles = null;
    }

    private void Triangulation()
    {
        var builder = new NTS.Triangulate.ConformingDelaunayTriangulationBuilder();
        //var builder = new NTS.Triangulate.DelaunayTriangulationBuilder();

        builder.SetSites(this.SquarePyramid);

        var triangles = builder.GetTriangles(new NTS.Geometries.GeometryFactory());
        Debug.Log($"Triangulation in NTS over: {triangles.ToString()}");

        // Arrange triangles in a proper way
        this.vertices = new Vector3[] {
            new Vector3(0, 0, 0),
            new Vector3(2, 0, 0),
            new Vector3(0, 2, 0),
            new Vector3(2, 2, 0),
            new Vector3(1, 1, 2)
        };

        var utriangles = new List<int>();
        for (int i = 0, l = triangles.Count; i < l; i++)
        {
            Geo.Geometries.IGeometry triangle = triangles[i];
            var points = triangle.Coordinates;
            for (int j = 0; j < points.Length - 1; j++) // The triangulator generates a loop, the last is the same as the first, we remove it
            {
                var stringPoint = $"{points[j].X} {points[j].Y} {points[j].Z}";
                utriangles.Add(this.dict[stringPoint]);
            }
        }

        this.triangles = utriangles.ToArray();

        this.printStatus();
    }

    private void printStatus()
    {
        var s = "";
        for (int i = 0; i < this.triangles.Length; i++)
        {
            s += $"{this.triangles[i]}, ";
        }
        Debug.Log($"Triangles in triangulation: {s}");

        s = "";
        for (int i = 0; i < this.dict.Keys.Count(); i++)
        {
            s += $"k:{this.dict.Keys.ElementAt(i)} v:{this.dict[this.dict.Keys.ElementAt(i)]}; ";
        }
        Debug.Log($"Dictionary: {s}");
    }
}
