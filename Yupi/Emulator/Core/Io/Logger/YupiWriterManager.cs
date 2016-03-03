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

namespace Yupi.Emulator.Core.Io.Logger
{
    /// <summary>
    ///     Class YupiWriterManager.
    /// </summary>
    public class YupiWriterManager
    {
        /// <summary>
        ///     The _m _disabled
        /// </summary>
        private static bool _disabled;

        /// <summary>
        ///     Gets or sets a value indicating whether [_disabled state].
        /// </summary>
        /// <value><c>true</c> if [_disabled state]; otherwise, <c>false</c>.</value>
        public static bool DisabledState
        {
            get { return _disabled; }
            set { _disabled = value; }
        }

        /// <summary>
        ///     Writes the line.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="header">The header.</param>
        /// <param name="color">The color.</param>
        public static void WriteLine(string format, string header, ConsoleColor color = ConsoleColor.White)
            => ConsoleOutputWriter.WriteLine(format, header, color);

        /// <summary>
        ///     Writes the specified format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="header">The header.</param>
        /// <param name="color">The color.</param>
        internal static void Write(string format, string header, ConsoleColor color = ConsoleColor.White)
            => ConsoleOutputWriter.Write(format, header, color);

        /// <summary>
        ///     Disables the primary writing.
        /// </summary>
        /// <param name="disableState">if set to <c>true</c> [clear console].</param>
        public static void DisablePrimaryWriting(bool disableState = true) => _disabled = disableState;
    }
}