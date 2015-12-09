using System;

namespace Yupi.Core.Io
{
    internal class ConsoleOutputWriter
    {
        /// <summary>
        /// Writes the line.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="header">The header.</param>
        /// <param name="color">The color.</param>
        internal static void WriteLine(string format, string header, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(@" ");// + @" [" + DateTime.Now + "] ");

            if (header != "")
            {
                Console.Write("[");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(header);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("]");
            }

            Console.Write(" >> ");
            Console.ForegroundColor = color;
            Console.WriteLine(format);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        /// <summary>
        /// Writes the specified format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="header">The header.</param>
        /// <param name="color">The color.</param>
        internal static void Write(string format, string header = "", ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(@"  " + @"[" + DateTime.Now + "] ");

            if (header != "")
            {
                Console.Write("[");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(header);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("] ");
            }

            Console.Write(" >> ");
            Console.ForegroundColor = color;
            Console.Write(format);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}