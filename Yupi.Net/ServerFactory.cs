using System;

namespace Yupi.Net
{
	public class ServerFactory
	{
		public static IServer CreateServer(int port) {
			IServerSettings settings = new ServerSettings() {
				IP = "Any",
				Port = port,
				MaxWorkingThreads = 0, // Let the OS decide
				MinWorkingThreads = 0,
				MinIOThreads = 0,
				MaxIOThreads  = 0,
				BufferSize = 4096,
				Backlog = 100,
				MaxConnections = 10000 // Maximum overall connections
			};

			return CreateServer(settings);
		}

		public static IServer CreateServer(IServerSettings settings) {
			CrossDomainSettings flashPolicy = new CrossDomainSettings("*", settings.Port);

			// TODO Add selection for SuperSocket vs DotNetty
			return new SuperSocketImpl.SuperServer (settings, flashPolicy);
		}
	}
}

