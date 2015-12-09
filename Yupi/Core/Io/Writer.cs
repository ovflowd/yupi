using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Text;

namespace Yupi.Core.Io
{
    /// <summary>
    /// Class Writer.
    /// </summary>
    public class Writer
    {
        /// <summary>
        /// The _m _disabled
        /// </summary>
        private static bool _disabled;

        /// <summary>
        /// Gets or sets a value indicating whether [_disabled state].
        /// </summary>
        /// <value><c>true</c> if [_disabled state]; otherwise, <c>false</c>.</value>
        public static bool DisabledState
        {
            get { return _disabled; }
            set { _disabled = value; }
        }

        /// <summary>
        /// Writes the line.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="header">The header.</param>
        /// <param name="color">The color.</param>
        public static void WriteLine(string format, string header, ConsoleColor color = ConsoleColor.White)
        {
            ConsoleOutputWriter.WriteLine(format, header, color);
        }

        /// <summary>
        /// Writes the line.
        /// </summary>
        /// <param name="line">The line.</param>
        /// <param name="colour">The colour.</param>
        internal static void WriteLine(string line, ConsoleColor colour = ConsoleColor.Yellow)
        {
            ConsoleOutputWriter.WriteLine(line, "Yupi.Exceptions", colour);
        }

        /// <summary>
        /// Writes the specified format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="header">The header.</param>
        /// <param name="color">The color.</param>
        internal static void Write(string format, string header = "", ConsoleColor color = ConsoleColor.White)
        {
            ConsoleOutputWriter.Write(format, header, color);
        }

        /// <summary>
        /// Logs the exception.
        /// </summary>
        /// <param name="logText">The log text.</param>
        public static void LogException(string logText)
        {
            WriteToFile(@"Logs\ExceptionsLog.txt", logText + "\r\n\r\n");
            WriteLine("An exception was registered.", ConsoleColor.Red);
        }

        /// <summary>
        /// Logs the critical exception.
        /// </summary>
        /// <param name="logText">The log text.</param>
        public static void LogCriticalException(string logText)
        {
            WriteToFile(@"Logs\ExceptionsLog.txt", logText + "\r\n\r\n");
            WriteLine("A critical exception was registered.", ConsoleColor.Red);
        }

        /// <summary>
        /// Logs the cache error.
        /// </summary>
        /// <param name="logText">The log text.</param>
        public static void LogCacheError(string logText)
        {
            WriteToFile(@"Logs\ErrorLog.txt", logText + "\r\n\r\n");
            WriteLine("A caching error was registered.", ConsoleColor.Red);
        }

        /// <summary>
        /// Logs the message.
        /// </summary>
        /// <param name="logText">The log text.</param>
        /// <param name="output">if set to <c>true</c> [output].</param>
        public static void LogMessage(string logText, bool output = true)
        {
            WriteToFile(@"Logs\CommonLog.txt", logText + "\r\n\r\n");

            if (output)
                Console.WriteLine(logText);
        }

        /// <summary>
        /// Logs the ddos.
        /// </summary>
        /// <param name="logText">The log text.</param>
        public static void LogDdos(string logText)
        {
            WriteToFile(@"Logs\DDosLog.txt", logText + "\r\n\r\n");
            WriteLine(logText, ConsoleColor.Red);
        }

        /// <summary>
        /// Logs the thread exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="threadName">Name of the thread.</param>
        public static void LogThreadException(string exception, string threadName)
        {
            WriteToFile(@"Logs\ErrorLog.txt", string.Concat("Error en thread ", threadName, ": \r\n", exception, "\r\n\r\n"));
            WriteLine("An thread error was registered, in thread: " + threadName, ConsoleColor.Red);
        }

        /// <summary>
        /// Logs the query error.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="query">The query.</param>
        public static void LogQueryError(Exception exception, string query)
        {
            WriteToFile(@"Logs\MySQLErrors.txt", string.Concat("The query error was in: \r\n", query, "\r\n", exception, "\r\n\r\n"));
            WriteLine("A MySQL exception was registered.", ConsoleColor.Red);
        }

        /// <summary>
        /// Logs the packet exception.
        /// </summary>
        /// <param name="packet">The packet.</param>
        /// <param name="exception">The exception.</param>
        public static void LogPacketException(string packet, string exception)
        {
            WriteToFile(@"Logs\PacketLogError.txt", "Error in packet " + packet + ": \r\n" + exception + "\r\n\r\n");
            WriteLine("A packet exception was registered.", ConsoleColor.Red);
        }

        /// <summary>
        /// Handles the exception.
        /// </summary>
        /// <param name="pException">The p exception.</param>
        /// <param name="pLocation">The p location.</param>
        public static void HandleException(Exception pException, string pLocation)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(string.Concat("Exception logged ", DateTime.Now.ToString(CultureInfo.InvariantCulture), " in ", pLocation, ":"));
            stringBuilder.AppendLine(pException.ToString());

            if (pException.InnerException != null)
            {
                stringBuilder.AppendLine("Inner exception:");
                stringBuilder.AppendLine(pException.InnerException.ToString());
            }

            if (pException.HelpLink != null)
            {
                stringBuilder.AppendLine("Help link:");
                stringBuilder.AppendLine(pException.HelpLink);
            }

            if (pException.Source != null)
            {
                stringBuilder.AppendLine("Source:");
                stringBuilder.AppendLine(pException.Source);
            }

            stringBuilder.AppendLine("Data:");

            foreach (DictionaryEntry dictionaryEntry in pException.Data)
                stringBuilder.AppendLine(string.Concat("  Key: ", dictionaryEntry.Key, "Value: ", dictionaryEntry.Value));

            stringBuilder.AppendLine("Message:");
            stringBuilder.AppendLine(pException.Message);

            if (pException.StackTrace != null)
            {
                stringBuilder.AppendLine("Stack trace:");
                stringBuilder.AppendLine(pException.StackTrace);
            }

            stringBuilder.AppendLine();
            stringBuilder.AppendLine();

            LogException(stringBuilder.ToString());
        }

        /// <summary>
        /// Disables the primary writing.
        /// </summary>
        /// <param name="clearConsole">if set to <c>true</c> [clear console].</param>
        public static void DisablePrimaryWriting(bool clearConsole)
        {
            _disabled = true;
        }

        /// <summary>
        /// Logs the shutdown.
        /// </summary>
        /// <param name="builder">The builder.</param>
        public static void LogShutdown(StringBuilder builder)
        {
            WriteToFile(@"Logs\shutdownlog.txt", builder.ToString());
        }

        /// <summary>
        /// Writes to file.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="content">The content.</param>
        private static void WriteToFile(string path, string content)
        {
            File.AppendAllText(path, Environment.NewLine + content, Encoding.ASCII);
        }
    }
}