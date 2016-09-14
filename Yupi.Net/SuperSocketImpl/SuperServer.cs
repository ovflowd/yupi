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

namespace Yupi.Net.SuperSocketImpl
{
    public class SuperServer<T> : AppServer<Session<T>, RequestInfo>, IServer<T>
    {
        private readonly CrossDomainSettings FlashPolicy;

        public SuperServer(IServerSettings settings, CrossDomainSettings flashPolicy)
            : base(new DefaultReceiveFilterFactory<FlashReceiveFilter, RequestInfo>())
        {
            FlashPolicy = flashPolicy;

            IRootConfig rootConfig = CreateRootConfig(settings);

            IServerConfig config = CreateServerConfig(settings);

            Setup(rootConfig, config, logFactory: new Log4NetLogFactory());

            base.NewRequestReceived += HandleRequest;

            base.NewSessionConnected += (Session<T> session) => OnConnectionOpened(session);

            base.SessionClosed += (Session<T> session, CloseReason value) => OnConnectionClosed(session);
        }

        public event MessageReceived<T> OnMessageReceived = delegate { };

        public event ConnectionOpened<T> OnConnectionOpened = delegate { };

        public event ConnectionClosed<T> OnConnectionClosed = delegate { };

        private void HandleRequest(Session<T> session, RequestInfo requestInfo)
        {
            if (requestInfo.IsFlashRequest)
            {
                session.Send(FlashPolicy.GetBytes());
                session.Disconnect();
            }
            else
            {
                OnMessageReceived(session, requestInfo.Body);
            }
        }

        private IServerConfig CreateServerConfig(IServerSettings settings)
        {
            ServerConfig config = new ServerConfig();
            config.Ip = settings.IP;
            config.Port = settings.Port;
            config.ReceiveBufferSize = settings.BufferSize;
            config.SendBufferSize = settings.BufferSize;
            config.ListenBacklog = settings.Backlog;
            config.MaxConnectionNumber = settings.MaxConnections;

            return config;
        }

        private IRootConfig CreateRootConfig(IServerSettings settings)
        {
            RootConfig rootConfig = new RootConfig();
            if (settings.MaxWorkingThreads != 0)
                rootConfig.MaxWorkingThreads = settings.MaxWorkingThreads;

            if (settings.MinWorkingThreads != 0)
                rootConfig.MinWorkingThreads = settings.MinWorkingThreads;

            if (settings.MaxIOThreads != 0)
                rootConfig.MaxCompletionPortThreads = settings.MaxIOThreads;

            if (settings.MinIOThreads != 0)
                rootConfig.MinCompletionPortThreads = settings.MinIOThreads;

            return rootConfig;
        }
    }
}