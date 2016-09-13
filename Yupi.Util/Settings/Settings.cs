using System;
using Config.Net;
using System.IO;
using System.Reflection;

namespace Yupi.Util.Settings
{
	public static class Settings
	{
		private static readonly string ConfigDir;

		static Settings ()
		{
			string assemblyLocation = Assembly.GetExecutingAssembly ().Location;
			string assemblyDir = Directory.GetParent (assemblyLocation).FullName;

			// TODO Get the directory structure sorted!
			assemblyDir = Directory.GetParent (assemblyDir).Parent.Parent.FullName;

			ConfigDir = Path.Combine (assemblyDir, "Config", "Settings");

			if (!Directory.Exists (ConfigDir)) {
				Directory.CreateDirectory (ConfigDir);
			}
		}

		public static string GetPath(string fileName) {
			return Path.Combine (ConfigDir, fileName);
		}
	}
}

