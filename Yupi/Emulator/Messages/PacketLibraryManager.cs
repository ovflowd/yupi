using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Yupi.Emulator.Core.Io.Logger;
using Yupi.Emulator.Core.Settings;
using Yupi.Emulator.Messages.Buffers;
using Yupi.Emulator.Messages.Handlers;
using Yupi.Emulator.Messages.Library;

namespace Yupi.Emulator.Messages
{
    internal static class PacketLibraryManager
    {
        /// <summary>
        ///     Paramless Delegate
        /// </summary>
        public delegate void ParamLess();

        /// <summary>
        ///     Incoming Requests
        /// </summary>
        internal static Dictionary<int, StaticRequestHandler> Incoming;

        /// <summary>
        ///     Incoming Request Library
        /// </summary>
        internal static Dictionary<string, string> Library;

        /// <summary>
        ///     Outgoing Request Library
        /// </summary>
        internal static Dictionary<string, int> Outgoing;

        /// <summary>
        ///     Number of Releases
        /// </summary>
        internal static int ReleasesCount;

        /// <summary>
        ///     Release Name
        /// </summary>
        internal static string ReleaseName;

        /// <summary>
        ///     Configure Dictionaries
        /// </summary>
        public static void Configure()
        {
            Incoming = new Dictionary<int, StaticRequestHandler>();
            Library = new Dictionary<string, string>();
            Outgoing = new Dictionary<string, int>();

            ReleaseName = ServerConfigurationSettings.Data["client.build"];
        }

        /// <summary>
        ///     Reload Dictionaries
        /// </summary>
        internal static void ReloadDictionarys()
        {
            Incoming.Clear();
            Outgoing.Clear();
            Library.Clear();

            Register();
        }

        /// <summary>
        ///     Register Dictionaries
        /// </summary>
        public static void Register()
        {
            RegisterLibrary();
            RegisterOutgoing();
            RegisterIncoming();
        }

        /// <summary>
        ///     Initialize
        /// </summary>
        public static void Init()
        {
            Configure();

            Register();
        }

        /// <summary>
        ///     Return Outgoing Packet Id
        /// </summary>
        public static int OutgoingHandler(string packetName)
        {
            int packetId;

            if (Outgoing.TryGetValue(packetName, out packetId))
                return packetId;

            YupiLogManager.LogWarning("Outgoing " + packetName + " doesn't exist.", "Yupi.Communication", false);

            return -1;
        }

        /// <summary>
        ///     Handle Incoming Request
        /// </summary>
        public static void ReceiveRequest(MessageHandler handler, SimpleClientMessageBuffer messageBuffer)
        {
            if (Incoming.ContainsKey(messageBuffer.Id))
            {
                if (Yupi.PacketDebugMode)
                    YupiWriterManager.WriteLine(
                        $"Handled: {messageBuffer.Id}: " + Environment.NewLine + messageBuffer + Environment.NewLine,
                        "Yupi.Incoming", ConsoleColor.DarkGreen);

                if (Incoming[messageBuffer.Id] == null)
                    return;

                StaticRequestHandler staticRequestHandler = Incoming[messageBuffer.Id];

                staticRequestHandler(handler);

                return;
            }

            if (Yupi.PacketDebugMode)
                YupiWriterManager.WriteLine(
                    $"Refused: {messageBuffer.Id}: " + Environment.NewLine + messageBuffer + Environment.NewLine,
                    "Yupi.Incoming", ConsoleColor.DarkYellow);
        }

        /// <summary>
        ///     Register Incoming Packets
        /// </summary>
        internal static void RegisterIncoming()
        {
            ReleasesCount = 0;

            string[] filePaths = Directory.GetFiles($@"{Yupi.YupiVariablesDirectory}\Packets\{ReleaseName}", "*.incoming");

            foreach (string[] fileContents in filePaths.Select(currentFile => File.ReadAllLines(currentFile, System.Text.Encoding.UTF8)))
            {
                ReleasesCount++;

                foreach (string[] fields in fileContents.Where(line => !string.IsNullOrEmpty(line) && !line.StartsWith("[")).Select(line => line.Replace(" ", string.Empty).Split('=')))
                {
                    string packetName = fields[0];

                    if (fields[1].Contains('/'))
                        fields[1] = fields[1].Split('/')[0];

                    int packetId = fields[1].ToLower().Contains('x') ? Convert.ToInt32(fields[1], 16) : Convert.ToInt32(fields[1]);

                    if (!Library.ContainsKey(packetName))
                        continue;

                    string libValue = Library[packetName];

                    if (Incoming.ContainsKey(packetId))
                        continue;

                    PacketLibrary.GetProperty del = (PacketLibrary.GetProperty) Delegate.CreateDelegate(typeof(PacketLibrary.GetProperty), typeof(PacketLibrary), libValue);

                    Incoming.Add(packetId, new StaticRequestHandler(del));
                }
            }
        }

        /// <summary>
        ///     Register Outgoing Packets
        /// </summary>
        internal static void RegisterOutgoing()
        {
            string[] filePaths = Directory.GetFiles($@"{Yupi.YupiVariablesDirectory}\Packets\{ReleaseName}", "*.outgoing");

            foreach (string[] fields in filePaths.Select(File.ReadAllLines).SelectMany(fileContents => fileContents.Where(line => !string.IsNullOrEmpty(line) && !line.StartsWith("[")).Select(line => line.Replace(" ", string.Empty).Split('='))))
            {
                if (fields[1].Contains('/'))
                    fields[1] = fields[1].Split('/')[0];

                string packetName = fields[0];

                int packetId = int.Parse(fields[1]);

                if (packetId == -1 || Outgoing.ContainsKey(packetName))
                    continue;

                Outgoing.Add(packetName, packetId);
            }
        }

        /// <summary>
        ///     Register Incoming Packet Identifiers (Library)
        /// </summary>
        internal static void RegisterLibrary()
        {
            string[] filePaths = Directory.GetFiles($@"{Yupi.YupiVariablesDirectory}\Packets\{ReleaseName}", "*.library");

            foreach (string[] fields in filePaths.Select(File.ReadAllLines).SelectMany(fileContents => fileContents.Select(line => line.Split('='))))
            {
                if (fields[1].Contains('/'))
                    fields[1] = fields[1].Split('/')[0];

                string incomingName = fields[0];

                string libraryName = fields[1];

                if (Library.ContainsKey(incomingName))
                    continue;

                Library.Add(incomingName, libraryName);
            }
        }

        internal delegate void StaticRequestHandler(MessageHandler handler);
    }
}