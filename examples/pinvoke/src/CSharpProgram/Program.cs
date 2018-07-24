using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace CSharpProgram
{
    public class Program
    {
        [StructLayout(LayoutKind.Sequential)]

        private struct MyStruct
        {
            public double a;
            public int b;
            public double c;
        }

        [DllImport("CPPLibrary.dll")]
        private static extern int fun1(int p1);

        [DllImport("CPPLibrary.dll")]
        private static extern int fun2(int[] p1, int l);

        [DllImport("CPPLibrary.dll")]
        private static extern IntPtr fun3();

        [DllImport("CPPLibrary.dll")]
        private static extern IntPtr fun4(int[] p1, int l);

        [DllImport("CPPLibrary.dll")]
        private static extern IntPtr fun5();

        [DllImport("CPPLibrary.dll")]
        private static extern int relmem(IntPtr arr);

        public static void Main()
        {
            Execute1();
            Execute2();
            Execute3();
            Execute5();

            Console.ReadLine();
        }

        private static void Execute1()
        {
            var p = 3;
            int res = fun1(p);

            Console.WriteLine($"1) Passed: {p}, got: {res}!");
        }

        private static void Execute2()
        {
            var p = new int[] { 3, 4, 5, 6 };
            int res = fun2(p, p.Length);

            Console.WriteLine($"2) Passed: {p}, got: {res}!");
        }

        private static void Execute3()
        {
            IntPtr res = fun3();
            int[] arr = ExtractArray(res);

            Console.WriteLine($"3) Passed: nothing, got: {arr.Select(n => $"{n}").Aggregate((a, b) => $"{a},{b}")}!");

            // Release memory
            if (relmem(res) != 0)
            {
                throw new Exception("Memory release failed on 3)");
            }
        }

        private static void Execute5()
        {
            IntPtr ptr = fun5();
            MyStruct s = (MyStruct)Marshal.PtrToStructure(ptr, typeof(MyStruct));

            Console.WriteLine($"2) Passed: Nothing, got: {s.a}, {s.b}, {s.c}!");
        }

        private static int[] ExtractArray(IntPtr ptr)
        {
            int arrayLength = Marshal.ReadInt32(ptr);
            ptr = IntPtr.Add(ptr, 4); // By 4 bytes
            int[] result = new int[arrayLength];
            Marshal.Copy(ptr, result, 0, arrayLength);

            return result;
        }
    }
}
