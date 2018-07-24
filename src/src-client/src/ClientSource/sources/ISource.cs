/// <summary>
/// Andrea Tino - 2018
/// </summary>

namespace CodeAlive.ClientSource
{
    internal interface ISource
    {
        /// <summary>
        /// Runs the program as emitted by the parser.
        /// </summary>
        void Run();

        /// <summary>
        /// Runs the original program, without parsing.
        /// </summary>
        void RunOriginal();
    }
}