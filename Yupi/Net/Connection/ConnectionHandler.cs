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

using System;
using Helios.Net;
using Helios.Topology;
using Yupi.Core.Encryption.Hurlant.Crypto.Prng;
using Yupi.Core.Io.Logger;
using Yupi.Messages.Parsers.Interfaces;

namespace Yupi.Net.Connection
{
    /// <summary>
    ///     Class ConnectionHandler.
    /// </summary>
    public class ConnectionHandler
    {
        /// <summary>
        ///     Connection Info
        /// </summary>
        public INode ConnectionInfo;

        /// <summary>
        ///     Connection Data
        /// </summary>
        public IConnection ConnectionChannel;

        /// <summary>
        ///     Connection Identifier
        /// </summary>
        public uint ConnectionId;

        /// <summary>
        ///     Data Parser
        /// </summary>
        public IDataParser DataParser;

        /// <summary>
        ///     The ar c4 client side
        /// </summary>
        internal Arc4 Arc4ClientSide;

        /// <summary>
        ///     The ar c4 server side
        /// </summary>
        internal Arc4 Arc4ServerSide;

        public ConnectionHandler(INode connectionInfo, IConnection connectionChannel, IDataParser dataParser, uint connectionId)
        {
            ConnectionInfo = connectionInfo;
            ConnectionChannel = connectionChannel;
            ConnectionId = connectionId;
            DataParser = dataParser;
        }

        private void OnReceive(NetworkData incomingData, IConnection responseChannel)
        {
            ConnectionManager.ServerPrint(responseChannel.RemoteHost, $"recieved {incomingData.Length} bytes");

            try
            {
                byte[] dataBytes = incomingData.Buffer;

                Arc4ServerSide?.Parse(ref dataBytes);

                DataParser.HandlePacketData(dataBytes, incomingData.Length);
            }
            finally
            {
                StartReceivingData();
            }
        }

        public void StartReceivingData() => ConnectionChannel.BeginReceive(OnReceive);

        public void SendData(byte[] dataBytes)
        {
            YupiWriterManager.WriteLine($"Sending: {dataBytes.Length} bytes.", "Yupi.Net");

            Arc4ClientSide?.Parse(ref dataBytes);

            try
            {
                NetworkData networkData = NetworkData.Create(ConnectionChannel.RemoteHost, dataBytes, dataBytes.Length);

                ConnectionChannel.Send(networkData);
            }
            catch
            {
                Disconnect();
            }      
        }

        public void Disconnect()
        {
            ConnectionChannel.Close();

            DataParser.Dispose();
        } 
    }
}