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
using log4net;

namespace Yupi.Core.Io
{
    /// <summary>
    ///     Class Writer.
    /// </summary>
    public class Writer
    {
        /// <summary>
        ///     The _m _disabled
        /// </summary>
        private static bool _disabled;

        private static readonly ILog LogManager = Yupi.GetLogManager();

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
        ///     Writes the line.
        /// </summary>
        /// <param name="line">The line.</param>
        /// <param name="colour">The colour.</param>
        internal static void WriteLine(string line, ConsoleColor colour = ConsoleColor.Yellow)
            => ConsoleOutputWriter.WriteLine(line, "Yupi.Exceptions", colour);

        /// <summary>
        ///     Writes the specified format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="header">The header.</param>
        /// <param name="color">The color.</param>
        internal static void Write(string format, string header, ConsoleColor color = ConsoleColor.White)
            => ConsoleOutputWriter.Write(format, header, color);

        /// <summary>
        ///     Logs the exception.
        /// </summary>
        /// <param name="logText">The log text.</param>
        public static void LogException(string logText)
        {
            LogManager.Error(logText);
            WriteLine("Registered Game Exception.", ConsoleColor.DarkRed);
        }

        /// <summary>
        ///     Logs the exception.
        /// </summary>
        /// <param name="pException"></param>
        /// <param name="pLocation"></param>
        public static void LogException(Exception pException, string pLocation)
        {
            LogManager.Error(pException);
            WriteLine("Registered Game Exception.", ConsoleColor.DarkRed);
        }

        /// <summary>
        ///     Logs the critical exception.
        /// </summary>
        /// <param name="logText">The log text.</param>
        public static void LogCriticalException(string logText)
        {
            LogManager.Fatal(logText);
            WriteLine("Registered Game Critical Exception.", ConsoleColor.Red);
        }

        /// <summary>
        ///     Logs the exception.
        /// </summary>
        /// <param name="pException"></param>
        /// <param name="pLocation"></param>
        public static void LogCriticalException(Exception pException, string pLocation)
        {
            LogManager.Fatal(pException);
            WriteLine("Registered Game Critical Exception.", ConsoleColor.Red);
        }

        /// <summary>
        ///     Logs the cache error.
        /// </summary>
        /// <param name="logText">The log text.</param>
        public static void LogCacheException(string logText)
        {
            LogManager.Error(logText);
            WriteLine("Registered Cache System Exception.", ConsoleColor.DarkMagenta);
        }

        /// <summary>
        ///     Logs the message.
        /// </summary>
        /// <param name="logText">The log text.</param>
        /// <param name="output">if set to <c>true</c> [output].</param>
        /// <param name="header"></param>
        public static void LogMessage(string logText, string header = "Yupi.Info", bool output = true)
        {
            LogManager.Info(logText);

            if (output)
                WriteLine(logText, header);
        }

        /// <summary>
        ///     Logs the thread exception.
        /// </summary>
        /// <param name="logText">The exception.</param>
        /// <param name="threadName">Name of the thread.</param>
        public static void LogThreadException(string logText, string threadName)
        {
            LogManager.Fatal(logText);
            WriteLine("Registered Game Thread Exception [#" + threadName + "].", ConsoleColor.DarkRed);
        }

        /// <summary>
        ///     Logs the query error.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="query">The query.</param>
        public static void LogMySqlException(Exception exception, string query)
        {
            LogManager.Error(exception);
            WriteLine("Registered MySQL Exception.", ConsoleColor.DarkYellow);
        }

        /// <summary>
        ///     Logs the packet exception.
        /// </summary>
        /// <param name="packet">The packet.</param>
        /// <param name="exception">The exception.</param>
        public static void LogPacketException(string packet, string exception)
        {
            LogManager.Error(exception);
            WriteLine("Registered Packet Handling Exception [#" + packet + "].", ConsoleColor.DarkMagenta);
        }

        /// <summary>
        ///     Disables the primary writing.
        /// </summary>
        /// <param name="clearConsole">if set to <c>true</c> [clear console].</param>
        public static void DisablePrimaryWriting(bool clearConsole) => _disabled = true;
    }
}