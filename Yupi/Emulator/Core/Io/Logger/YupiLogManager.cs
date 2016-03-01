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
using log4net.Config;
using log4net.Repository;

namespace Yupi.Emulator.Core.Io.Logger
{
    public class YupiLogManager
    {
        private static ILog _yupiLogManager;

        public static void Init(Type declaringType)
        {
            XmlConfigurator.Configure();

            _yupiLogManager = LogManager.GetLogger(declaringType);
        }

        public static void Stop()
        {
            ILoggerRepository repository = LogManager.GetRepository();

            repository.Shutdown();
        }

        /// <summary>
        ///     Logs the exception.
        /// </summary>
        /// <param name="exceptionText">The log text.</param>
        /// <param name="writerBody"></param>
        /// <param name="writerHeader"></param>
        public static void LogException(string exceptionText, string writerBody = "Registered HabboHotel Exception.", string writerHeader = "Yupi.Fail")
        {
            lock (_yupiLogManager)
                _yupiLogManager.Error(exceptionText);

            YupiWriterManager.WriteLine(writerBody, writerHeader, ConsoleColor.DarkRed);
        }

        /// <summary>
        ///     Logs the exception.
        /// </summary>
        /// <param name="exceptionLog"></param>
        /// <param name="writerBody"></param>
        /// <param name="writerHeader"></param>
        public static void LogException(Exception exceptionLog, string writerBody = "Registered HabboHotel Exception.", string writerHeader = "Yupi.Fail")
        {
            lock (_yupiLogManager)
                _yupiLogManager.Error(exceptionLog);
        }

        /// <summary>
        ///     Logs the critical exception.
        /// </summary>
        /// <param name="exceptionText">The log text.</param>
        /// <param name="writerBody"></param>
        /// <param name="writerHeader"></param>
        public static void LogCriticalException(string exceptionText, string writerBody = "Registered HabboHotel Critical Exception.", string writerHeader = "Yupi.Fail")
        {
            lock (_yupiLogManager)
                _yupiLogManager.Fatal(exceptionText);

            YupiWriterManager.WriteLine(writerBody, writerHeader, ConsoleColor.Red);
        }

        /// <summary>
        ///     Logs the exception.
        /// </summary>
        /// <param name="exceptionLog"></param>
        /// <param name="writerBody"></param>
        /// <param name="writerHeader"></param>
        public static void LogCriticalException(Exception exceptionLog, string writerBody = "Registered HabboHotel Critical Exception.", string writerHeader = "Yupi.Fail")
        {
            lock (_yupiLogManager)
                _yupiLogManager.Fatal(exceptionLog);

            YupiWriterManager.WriteLine(writerBody, writerHeader, ConsoleColor.Red);
        }

        /// <summary>
        ///     Logs the message.
        /// </summary>
        /// <param name="logText">The log text.</param>
        /// <param name="writerHeader"></param>
        /// <param name="output"></param>
        public static void LogWarning(string logText, string writerHeader = "Yupi.Info", bool output = true)
        {
            lock (_yupiLogManager)
                _yupiLogManager.Warn(logText);

            if (output)
                YupiWriterManager.WriteLine(logText, writerHeader, ConsoleColor.DarkYellow);
        }

        /// <summary>
        ///     Logs the message.
        /// </summary>
        /// <param name="logText">The log text.</param>
        /// <param name="writerHeader"></param>
        /// <param name="output"></param>
        public static void LogMessage(string logText, string writerHeader = "Yupi.Info", bool output = true)
        {
            lock (_yupiLogManager)
                _yupiLogManager.Info(logText);

            if(output)
                YupiWriterManager.WriteLine(logText, writerHeader, ConsoleColor.DarkGreen);
        }

        public static ILog GetLogManager() => _yupiLogManager;
    }
}
