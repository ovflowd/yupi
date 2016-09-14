using Yupi.Net.SuperSocketImpl;

namespace Yupi.Net
{
    public class ServerFactory<T>
    {
        public static IServer<T> CreateServer(int port)
        {
            IServerSettings settings = new ServerSettings
            {
                IP = "Any",
                Port = port,
                MaxWorkingThreads = 0, // Let the OS decide
                MinWorkingThreads = 0,
                MinIOThreads = 0,
                MaxIOThreads = 0,
                BufferSize = 4096,
                Backlog = 100,
                MaxConnections = 10000 // Maximum overall connections
            };

            return CreateServer(settings);
        }

        public static IServer<T> CreateServer(IServerSettings settings)
        {
            var flashPolicy = new CrossDomainSettings("*", settings.Port);

            // TODO Add selection for SuperSocket vs DotNetty
            return new SuperServer<T>(settings, flashPolicy);
        }
    }
}