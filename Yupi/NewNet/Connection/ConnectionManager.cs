using System.Net;
using Helios.Reactor;
using Helios.Reactor.Bootstrap;
using Helios.Topology;
using Yupi.Messages.Parsers.Interfaces;

namespace Yupi.NewNet.Connection
{
    public class ConnectionManager
    {
        public IServerFactory ServerFactory;

        public IReactor Reactor;

        /// <summary>
        ///     The _parser
        /// </summary>
        public IDataParser DataParser;

        /// <summary>
        ///     Count of accepeted connections
        /// </summary>
        public uint AcceptedConnections;

        public void Init(int serverPort, int maxConnections, int maxConnectionsPerIp, IDataParser initialPacketData)
        {
            AcceptedConnections = 0;

            DataParser = initialPacketData;

            ServerFactory = new ServerBootstrap().SetTransport(TransportType.Tcp).Build();

            Reactor = ServerFactory.NewReactor(NodeBuilder.BuildNode().Host(IPAddress.Any).WithPort(serverPort));
        }

        /// <summary>
        ///     Destroys this instance.
        /// </summary>
        public void Destroy() => Reactor.Stop();
    }
}
