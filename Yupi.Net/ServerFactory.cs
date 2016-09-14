namespace Yupi.Net
{
    using System;

    public class ServerFactory<T>
    {
        #region Methods

        public static IServer<T> CreateServer(int port)
        {
            IServerSettings settings = new ServerSettings()
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
            CrossDomainSettings flashPolicy = new CrossDomainSettings("*", settings.Port);

            // TODO Add selection for SuperSocket vs DotNetty
            return new SuperSocketImpl.SuperServer<T>(settings, flashPolicy);
        }

        #endregion Methods
    }
}