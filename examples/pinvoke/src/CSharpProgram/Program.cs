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

        public static void Main()
        {
            Execute1();
            Execute2();

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
    }
}
