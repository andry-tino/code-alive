using System;

using NTS = NetTopologySuite;
using Geo = GeoAPI;

namespace TriangulationExample
{
    public class Program
    {
        public static void Main()
        {
            Triangulation();

            ConvexHull();

            Console.WriteLine("Press a key to exit...");
            Console.ReadLine();
        }

        private static Geo.Geometries.IGeometry SquarePyramid
        {
            get
            {
                return new NTS.Geometries.MultiPoint(new[] {
                    new NTS.Geometries.Point(0, 0, 0),
                    new NTS.Geometries.Point(2, 0, 0),
                    new NTS.Geometries.Point(0, 2, 0),
                    new NTS.Geometries.Point(2, 2, 0),
                    new NTS.Geometries.Point(1, 1, 2)
                });
            }
        }

        private static void Triangulation()
        {
            //var builder = new NTS.Triangulate.ConformingDelaunayTriangulationBuilder();
            var builder = new NTS.Triangulate.DelaunayTriangulationBuilder();
            
            builder.SetSites(SquarePyramid);

            var triangles = builder.GetTriangles(new NTS.Geometries.GeometryFactory());
            var subd = builder.GetSubdivision();

            Console.WriteLine("Triangles: " + triangles.ToString());

            //foreach (var v in subd) Console.WriteLine("QuadSubd: " + v.ToString());
        }

        private static void ConvexHull()
        {
            var hull = new NTS.Algorithm.ConvexHull(SquarePyramid);
            var chull = hull.GetConvexHull();

            Console.WriteLine("Convex hull:" + chull.ToString());
        }
    }
}
