/// <summary>
/// Andrea Tino - 2018
/// </summary>

namespace DelaunayTriangulator.IntegrationTest
{
    using System;
    using System.Linq;
    using System.Runtime.InteropServices;

    public class Tests
    {
        private const string DllPath = "DelaunayTriangulator.dll";

        #region Declarations for interop

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

        public static void Main(string[] args)
        {
            // Perform a triangulation
            ExecuteSafe("codealive_triangulator_perform", () => codealive_triangulator_perform(30));

            // Get vertices
            int verticesNum = 0;
            ExecuteSafe("codealive_triangulator_get_vertices_num", () => verticesNum = codealive_triangulator_get_vertices_num());
            Console.WriteLine($"Got {verticesNum} vertices");

            Console.WriteLine("Vertices:");
            for (int i = 0; i < verticesNum; i++)
            {
                double x = codealive_triangulator_get_vertex(i, 0);
                double y = codealive_triangulator_get_vertex(i, 1);
                double z = codealive_triangulator_get_vertex(i, 2);
                
                Console.WriteLine($"- Vertex: ({x}, {y}, {z})");
            }

            // Get triangles
            int trianglesLen = 0;
            ExecuteSafe("codealive_triangulator_get_triangles_vlen", () => trianglesLen = codealive_triangulator_get_triangles_vlen());
            Console.WriteLine($"Got {trianglesLen / 3} triangles (len: {trianglesLen})");

            int[] triangles = null;
            ExecuteSafe("codealive_triangulator_get_triangles", 
                () => triangles = ExtractArray(codealive_triangulator_get_triangles(), trianglesLen));
            Console.WriteLine($"Extracted triangles array is: {triangles.Select(n => n.ToString()).Aggregate((n1, n2) => $"{n1}, {n2}")}");

            // Destroy the triangulation
            ExecuteSafe("codealive_triangulator_dispose", () => codealive_triangulator_dispose());

            // Press enter to exit
            Console.ReadLine();
        }

        private static void ExecuteSafe(string name, Action command)
        {
            try
            {
                command();
                Console.WriteLine($"Successfully executed command `{name}`!");
            }
            catch
            {
                Console.WriteLine($"Error executing command `{name}`!");
            }
            Console.WriteLine(string.Empty);
        }

        private static int[] ExtractArray(IntPtr ptr, int arrayLength)
        {
            int[] result = new int[arrayLength];
            Marshal.Copy(ptr, result, 0, arrayLength);

            return result;
        }
    }
}
