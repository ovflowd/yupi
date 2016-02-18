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

using System.Net;
using DotNetty.Transport.Channels;
using Yupi.Emulator.Messages.Parsers;

namespace Yupi.Emulator.Net.Connection
{
    /// <summary>
    ///     Class ConnectionActor.
    /// </summary>
    public class ConnectionActor
    {
        /// <summary>
        ///     Data Parser
        /// </summary>
        public ServerPacketParser DataParser;

        /// <summary>
        ///     Connection Id
        /// </summary>
        public string ConnectionId;

        /// <summary>
        ///     IP Address
        /// </summary>
        public string IpAddress;

        /// <summary>
        ///    Connection Channel
        /// </summary>
        public IChannel ConnectionChannel;

        /// <summary>
        ///     Is in HandShake Process
        /// </summary>
        internal bool HandShakeCompleted;

        /// <summary>
        ///     Is in HandShake Process
        /// </summary>
        internal bool HandShakePartialCompleted;

        /// <summary>
        ///     Count of Same IP Connection.
        /// </summary>
        internal int SameHandledCount;

        public ConnectionActor(ServerPacketParser dataParser, IChannel context)
        {
            DataParser = dataParser;

            ConnectionChannel = context;

            ConnectionId = context.Id.ToString();

            IpAddress = (context.RemoteAddress as IPEndPoint)?.Address.ToString();

            SameHandledCount = 0;

            HandShakeCompleted = false;

            HandShakePartialCompleted = false;
        }

        public void Close()
        {
            Yupi.GetGame().GetClientManager().RemoveClient(ConnectionId);

            DataParser.Dispose();

            ConnectionChannel.DisconnectAsync();

            ConnectionChannel.CloseAsync();

            ConnectionChannel.Flush();
        }
    }
}