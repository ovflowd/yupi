using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Yupi.Messages
{
    public class PacketLibrary
    {
        private static readonly log4net.ILog Logger = log4net.LogManager
            .GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly string ConfigDir;

        private Dictionary<string, short> Incoming;
        private Dictionary<string, short> Outgoing;

        private readonly string Release;

        public PacketLibrary(string release, string configDir)
        {
            Release = release;
            ConfigDir = configDir;
            Reload();
        }

        public void Reload()
        {
            LoadIncoming();
            LoadOutgoing();
        }

        public short GetIncomingId(string name)
        {
            short id;

            Incoming.TryGetValue(name, out id);

            if (id == default(short)) Logger.WarnFormat("Unknown incoming message: {0}", name);

            return id;
        }

        public short GetOutgoingId(string name)
        {
            short id;

            Outgoing.TryGetValue(name, out id);

            if (id == default(short)) Logger.WarnFormat("Unknown outgoing message: {0}", name);

            return id;
        }

        private void LoadIncoming()
        {
            ReadFiles("*.incoming", ref Incoming);
        }

        private void LoadOutgoing()
        {
            ReadFiles("*.outgoing", ref Outgoing);
        }

        private void ReadFiles(string pattern, ref Dictionary<string, short> packets)
        {
            packets = new Dictionary<string, short>();

            var path = Path.Combine(ConfigDir, "Packets", Release);

            var files = Directory.GetFiles(path, pattern);

            foreach (var file in files) ReadIni(file, packets);
        }

        private void ReadIni(string file, Dictionary<string, short> packets)
        {
            IniFile ini = new IniFile();
            ini.Load(file);

            foreach (IniSection section in ini.Sections)
                foreach (IniKey key in section.Keys)
                {
                    short id;
                    key.TryParseValue<short>(out id);

                    if (id == default(short)) continue;

                    packets.Add(key.Name, id);
                }
        }
    }
}