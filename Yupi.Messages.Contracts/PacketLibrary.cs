using System;
using System.Collections.Generic;
using System.IO;
using MadMilkman.Ini;

namespace Yupi.Messages
{
	public class PacketLibrary
	{
		private static readonly log4net.ILog Logger = log4net.LogManager
			.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		private Dictionary<string, short> Incoming;
		private Dictionary<string, short> Outgoing;

		private string Release;
		private string ConfigDir;

		public PacketLibrary (string release, string configDir)
		{
			this.Release = release;
			this.ConfigDir = configDir;
			Reload ();
		}

		public void Reload() {
			LoadIncoming ();
			LoadOutgoing ();
		}

		public short GetIncomingId(string name) {
			short id;

			Incoming.TryGetValue (name, out id);

			if (id == default(short)) {
				Logger.WarnFormat ("Unknown incoming message: {0}", name);
			}

			return id;
		}

		public short GetOutgoingId(string name) {
			short id;

			Outgoing.TryGetValue (name, out id);

			if (id == default(short)) {
				Logger.WarnFormat ("Unknown outgoing message: {0}", name);
			}

			return id;
		}

		private void LoadIncoming() {
			ReadFiles ("*.incoming", ref Incoming);
		}

		private void LoadOutgoing() {
			ReadFiles ("*.outgoing", ref Outgoing);
		}

		private void ReadFiles(string pattern, ref Dictionary<string, short> packets) {
			packets = new Dictionary<string, short> ();

			string path = Path.Combine (ConfigDir, "Packets", Release);

			string[] files = Directory.GetFiles(path, pattern);

			foreach (string file in files) {
				ReadIni (file, packets);
			}
		}

		private void ReadIni(string file, Dictionary<string, short> packets) {
			IniFile ini = new IniFile();
			ini.Load(file);

			foreach(IniSection section in ini.Sections) {
				foreach (IniKey key in section.Keys) {
					short id;
					key.TryParseValue<short> (out id);

					if (id == default(short)) {
						// TODO Log warning
						continue;
					}

					packets.Add (key.Name, id);
				}
			}
		}
	}
}

