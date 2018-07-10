using System;
using System.Runtime.InteropServices;

namespace CSharpProgram
{
    public class Program
    {
        [DllImport("CPPLibrary.dll")]
        private static extern int fun1(int p1);

        [DllImport("CPPLibrary.dll")]
        private static extern int fun2(int[] p1, int l);

        [DllImport("CPPLibrary.dll")]
        private static extern IntPtr fun3();

        [DllImport("CPPLibrary.dll")]
        private static extern IntPtr fun4(int[] p1, int l);

        [DllImport("CPPLibrary.dll")]
        private static extern int relmem(IntPtr arr);

        public static void Main()
        {
            Execute1();
            Execute2();
            Execute3();

            Console.ReadLine();
        }

        private static void Execute1()
        {
            var p = 3;
            var res = fun1(p);

            Console.WriteLine($"Passed: {p}, got: {res}!");
        }

        private static void Execute2()
        {
            var p = new int[] { 3, 4, 5, 6 };
            var res = fun2(p, p.Length);

            Console.WriteLine($"Passed: {p}, got: {res}!");
        }

        private static void Execute3()
        {
            var res = fun3();

            Console.WriteLine($"Passed: nothing, got: {res}!");

            // Release memory
            var relmemres = relmem(res);
            Console.WriteLine($"Releasing memory: {relmemres}.");
        }
    }
}
