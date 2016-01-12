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
using System.Reflection;
using Yupi.Core.Io;

namespace Yupi.Data
{
    /// <summary>
    ///     Class Logging.
    /// </summary>
    public static class ServerLogManager
    {
        /// <summary>
        ///     Gets or sets a value indicating whether [disabled state].
        /// </summary>
        /// <value><c>true</c> if [disabled state]; otherwise, <c>false</c>.</value>
        internal static bool DisabledState
        {
            get { return Writer.DisabledState; }
            set { Writer.DisabledState = value; }
        }

        /// <summary>
        ///     Logs the query error.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="query">The query.</param>
        public static void LogMySqlException(Exception exception, string query)
            => Writer.LogMySqlException(exception, query);

        /// <summary>
        ///     Logs the exception.
        /// </summary>
        /// <param name="logText">The log text.</param>
        internal static void LogException(string logText) => Writer.LogException($"{logText}");

        /// <summary>
        ///     Logs the exception.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="p"></param>
        internal static void LogException(Exception e, string p) => Writer.LogException(e, p);

        /// <summary>
        ///     Logs the exception.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="p"></param>
        internal static void LogException(Exception e, MethodBase p) => Writer.LogException(e, $"{p.DeclaringType.FullName}.{p.Name}");

        /// <summary>
        ///     Logs the critical exception.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="p"></param>
        internal static void LogCriticalException(Exception e, string p) => Writer.LogCriticalException(e, p);

        /// <summary>
        ///     Logs the critical exception.
        /// </summary>
        /// <param name="logText">The log text.</param>
        internal static void LogCriticalException(string logText) => Writer.LogCriticalException(logText);

        /// <summary>
        ///     Logs the cache error.
        /// </summary>
        /// <param name="logText">The log text.</param>
        internal static void LogCacheException(string logText) => Writer.LogCacheException(logText);

        /// <summary>
        ///     Logs the message.
        /// </summary>
        /// <param name="logText">The log text.</param>
        internal static void LogMessage(string logText) => Writer.LogMessage(logText);

        /// <summary>
        ///     Logs the thread exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="threadname">The threadname.</param>
        internal static void LogThreadException(string exception, string threadname)
            => Writer.LogThreadException(exception, threadname);

        /// <summary>
        ///     Logs the packet exception.
        /// </summary>
        /// <param name="packet">The packet.</param>
        /// <param name="exception">The exception.</param>
        internal static void LogPacketException(string packet, string exception)
            => Writer.LogPacketException(packet, exception);

        /// <summary>
        ///     Disables the primary writing.
        /// </summary>
        /// <param name="clearConsole">if set to <c>true</c> [clear console].</param>
        internal static void DisablePrimaryWriting(bool clearConsole) => Writer.DisablePrimaryWriting(clearConsole);
    }
}