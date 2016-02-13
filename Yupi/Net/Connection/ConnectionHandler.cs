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
using Yupi.Core.Security;
using Yupi.Net.Packets;

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
        private readonly INode _connectionInfo;

        /// <summary>
        ///     Connection Identifier
        /// </summary>
        public uint ConnectionId;

        /// <summary>
        ///     Get Ip Address
        /// </summary>
        public string GetIp() => _connectionInfo.Host.ToString();

        /// <summary>
        ///     Data Parser
        /// </summary>
        public ServerPacketParser DataParser;

        /// <summary>
        ///     Get Connection By Connection Info
        /// </summary>
        public IConnection GetResponseChannel() => _connectionInfo.GetConnection();

        /// <summary>
        ///     The ar c4 client side
        /// </summary>
        internal Arc4 Arc4ClientSide;

        /// <summary>
        ///     The ar c4 server side
        /// </summary>
        internal Arc4 Arc4ServerSide;

        public ConnectionHandler(INode connectionInfo, ServerPacketParser dataParser, uint connectionId)
        {
            _connectionInfo = connectionInfo;

            ConnectionId = connectionId;
            DataParser = dataParser;    
        }

        public void OnReceive(NetworkData incomingData, IConnection responseChannel)
        {
            ConnectionManager.ServerPrint(responseChannel.RemoteHost, $"recieved {incomingData.Length} bytes.");

            try
            {
                byte[] dataBytes = incomingData.Buffer;

                Arc4ServerSide?.Parse(ref dataBytes);

                DataParser.HandlePacketData(dataBytes, dataBytes.Length);
            }
            catch (Exception reason)
            {
                ConnectionManager.OnError(reason, responseChannel);
            }
            finally
            {
                StartReceivingData(responseChannel);
            }
        }

        public void StartReceivingInitialData(IConnection responseChannel)
        {
            responseChannel.BeginReceive(OnInitialReceive);
        } 

        public void OnInitialReceive(NetworkData incomingData, IConnection responseChannel)
        {
            ConnectionManager.ServerPrint(responseChannel.RemoteHost, $"initial received {incomingData.Length} bytes.");

            try
            {
                byte[] dataBytes = incomingData.Buffer;

                Arc4ServerSide?.Parse(ref dataBytes);

                if (dataBytes[0] == 60)
                    SendData(responseChannel, CrossDomainSettings.XmlPolicyBytes);
                else if (dataBytes[0] != 67)
                    SwitchToNormalReceive(responseChannel, dataBytes, dataBytes.Length);
            }
            catch (Exception reason)
            {
                ConnectionManager.OnError(reason, responseChannel);
            }
            finally
            {
                StartReceivingInitialData(responseChannel);
            }
        }

        public void SwitchToNormalReceive(IConnection responseChannel, byte[] dataBytes, int amountOfBytes)
        {
            try
            {
                DataParser.SetConnection(this, Yupi.GetGame().GetClientManager().GetClient(ConnectionId));

                DataParser.HandlePacketData(dataBytes, amountOfBytes);
            }
            catch (Exception reason)
            {
                ConnectionManager.OnError(reason, responseChannel);
            }
            finally
            {
                StartReceivingData(responseChannel);
            }
        }

        public void StartReceivingData(IConnection responseChannel) => responseChannel.BeginReceive(OnReceive);

        public void SendData(IConnection responseChannel, byte[] dataBytes)
        {
            ConnectionManager.ServerPrint(responseChannel.RemoteHost, $"sending {dataBytes.Length} bytes.");

            Arc4ClientSide?.Parse(ref dataBytes);

            try
            {
                responseChannel.Send(new NetworkData { Buffer = dataBytes, Length = dataBytes.Length, RemoteHost = responseChannel.RemoteHost });
            }
            catch
            {
                ConnectionManager.ServerPrint(responseChannel.RemoteHost, "failed sending data.");

                Disconnect(responseChannel);
            }      
        }

        public void Disconnect(IConnection responseChannel)
        {
            ConnectionManager.ServerPrint(responseChannel.RemoteHost, "Disconnection Called..");

            responseChannel.Close();

            DataParser.Dispose();
        } 
    }
}