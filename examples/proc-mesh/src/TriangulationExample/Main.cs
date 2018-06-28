using System;

using NTS = NetTopologySuite;
using Geo = GeoAPI;

namespace TriangulationExample
{
    public class Program
    {
        public static void Main()
        {
            var builder = new NTS.Triangulate.ConformingDelaunayTriangulationBuilder();

            // A (square) pyramid
            builder.SetSites(new NTS.Geometries.MultiPoint(new[] {
                new NTS.Geometries.Point(0, 0, 0),
                new NTS.Geometries.Point(2, 0, 0),
                new NTS.Geometries.Point(0, 2, 0),
                new NTS.Geometries.Point(2, 2, 0),
                new NTS.Geometries.Point(1, 1, 2)
            }));

            var triangles = builder.GetTriangles(new NTS.Geometries.GeometryFactory());

            Console.WriteLine(triangles.ToString());
            Console.WriteLine("Press a key to exit...");
            Console.ReadLine();
        }
    }
}
