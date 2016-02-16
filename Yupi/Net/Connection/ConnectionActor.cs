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
using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using Yupi.Core.Security;
using Yupi.Net.Packets;

namespace Yupi.Net.Connection
{
    /// <summary>
    ///     Class ConnectionActor.
    /// </summary>
    public class ConnectionActor
    {
        /// <summary>
        ///     Connection Info
        /// </summary>
        public EndPoint IpAddress;

        /// <summary>
        ///     Server Chanbel Identifier
        /// </summary>
        public IChannel Channel;

        /// <summary>
        ///     Data Parser
        /// </summary>
        public ServerPacketParser DataParser;

        /// <summary>
        ///     Is in HandShake Process
        /// </summary>
        internal bool HandShakeCompleted;

        /// <summary>
        ///     Is in HandShake Process
        /// </summary>
        internal bool HandShakePartialCompleted;

        public ConnectionActor(IChannel clientChannel, ServerPacketParser dataParser)
        {
            IpAddress = clientChannel.RemoteAddress;

            DataParser = dataParser;

            Channel = clientChannel;

            HandShakeCompleted = false;

            HandShakePartialCompleted = false;
        }

        public void OnReceive(IByteBuffer dataBuffer)
        {
            byte[] dataBytes = dataBuffer.ToArray();

            if (!HandShakeCompleted)
            {
                if (dataBytes[0] == 60 && !HandShakePartialCompleted)
                {
                    SendData(CrossDomainSettings.XmlPolicyBytes);

                    HandShakePartialCompleted = true;

                    return;
                }

                if (dataBytes[0] != 67 && HandShakePartialCompleted)
                    HandShakeCompleted = true;
            }

            DataParser.HandlePacketData(dataBytes, dataBytes.Length);
        }

        public void SendData(byte[] message)
        {
            Channel.WriteAsync(message);
        }

        public void Disconnect()
        {
            Channel.CloseAsync();

            DataParser.Dispose();
        } 
    }
}