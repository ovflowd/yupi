/**
     Because i love chocolat...                                      
                                    88 88  
                                    "" 88  
                                       88  
8b       d8 88       88 8b,dPPYba,  88 88  
`8b     d8' 88       88 88P'    "8a 88 88  
 `8b   d8'  88       88 88       d8 88 ""  
  `8b,d8'   "8a,   ,a88 88b,   ,a8" 88 aa  
    Y88'     `"YbbdP'Y8 88`YbbdP"'  88 88  
    d8'                 88                 
   d8'                  88     
   
   Private Habbo Hotel Emulating System
   @author Claudio A. Santoro W.
   @author Kessiler R.
   @version dev-beta
   @license MIT
   @copyright Sulake Corporation Oy
   @observation All Rights of Habbo, Habbo Hotel, and all Habbo contents and it's names, is copyright from Sulake
   Corporation Oy. Yupi! has nothing linked with Sulake. 
   This Emulator is Only for DEVELOPMENT uses. If you're selling this you're violating Sulakes Copyright.
*/

using System;

namespace Yupi.Core.Io
{
    internal class ConsoleOutputWriter
    {
        /// <summary>
        ///     Writes the line.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="header">The header.</param>
        /// <param name="color">The color.</param>
        internal static void WriteLine(string format, string header, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(@" ");

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
        ///     Writes the specified format.
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