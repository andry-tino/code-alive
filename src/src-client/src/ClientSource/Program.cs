/// <summary>
/// Andrea Tino - 2018
/// </summary>

namespace CodeAlive.ClientSource
{
    using System;

    public class Program
    {
        public static void Main(string[] args)
        {
            var src = CreateSource();

            src.Run();
        }

        private static ISource CreateSource()
        {
            return new Basic.SourceRunner();
        }
    }
}
