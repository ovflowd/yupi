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
using System.Net;
using System.Net.Sockets;
using Yupi.Core.Encryption.Hurlant.Crypto.Prng;
using Yupi.Data;
using Yupi.Messages.Parsers;
using Yupi.Net.Sockets;

namespace Yupi.Net.Connection
{
    /// <summary>
    ///     Class ConnectionData.
    /// </summary>
    public class ConnectionData : IDisposable
    {
        /// <summary>
        ///     Delegate OnClientDisconnectedEvent
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="exception">The exception.</param>
        public delegate void OnClientDisconnectedEvent(ConnectionData connection, Exception exception);

        /// <summary>
        ///     The _buffer
        /// </summary>
        private readonly byte[] _buffer;

        /// <summary>
        ///     The _remote end point
        /// </summary>
        private readonly EndPoint _remoteEndPoint;

        /// <summary>
        ///     The _is connected
        /// </summary>
        private bool _connected;

        /// <summary>
        ///     The _socket
        /// </summary>
        private Socket _socket;

        /// <summary>
        ///     The ar c4 client side
        /// </summary>
        internal Arc4 Arc4ClientSide;

        /// <summary>
        ///     The ar c4 server side
        /// </summary>
        internal Arc4 Arc4ServerSide;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ConnectionData" /> class.
        /// </summary>
        /// <param name="socket">The socket.</param>
        /// <param name="parser">The parser.</param>
        /// <param name="channelId">The channel identifier.</param>
        public ConnectionData(Socket socket, IDataParser parser, uint channelId)
        {
            _socket = socket;
            socket.SendBufferSize = SocketConnectionSettings.BufferSize;
            Parser = parser;
            _buffer = new byte[SocketConnectionSettings.BufferSize];
            _remoteEndPoint = socket.RemoteEndPoint;
            _connected = true;
            ChannelId = channelId;
        }

        /// <summary>
        ///     Identity of this channel
        /// </summary>
        /// <value>The channel identifier.</value>
        /// <remarks>Must be unique within a server.</remarks>
        public uint ChannelId { get; }

        /// <summary>
        ///     Gets or sets the parser.
        /// </summary>
        /// <value>The parser.</value>
        public IDataParser Parser { get; set; }

        /// <summary>
        ///     Gets or sets the disconnected.
        /// </summary>
        /// <value>The disconnected.</value>
        public OnClientDisconnectedEvent Disconnected
        {
            get { return DisconnectAction; }

            set
            {
                if (value == null)
                    DisconnectAction = (x, e) => { };
                else
                    DisconnectAction = value;
            }
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Disconnect();
        }

        /// <summary>
        ///     Occurs when [disconnect action].
        /// </summary>
        public event OnClientDisconnectedEvent DisconnectAction = delegate { };

        /// <summary>
        ///     Reads the asynchronous.
        /// </summary>
        private void ReadAsync()
        {
            try
            {
                _socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, OnReadCompleted, _socket);
            }
            catch (Exception e)
            {
                HandleDisconnect(SocketError.ConnectionReset, e);
            }
        }

        /// <summary>
        ///     Handles the disconnect.
        /// </summary>
        /// <param name="socketError">The socket error.</param>
        /// <param name="exception">The exception.</param>
        private void HandleDisconnect(SocketError socketError, Exception exception)
        {
            try
            {
                if (_socket != null && _socket.Connected)
                {
                    try
                    {
                        _socket.Shutdown(SocketShutdown.Both);
                        _socket.Close();
                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                }

                _connected = false;
                Parser.Dispose();

                SocketConnectionCheck.FreeConnection(GetIp());

                DisconnectAction(this, exception);
            }
            catch (Exception ex)
            {
                ServerLogManager.LogException(ex, "Yupi.Net.Connection.ConnectionData.ConnectionInformation");
            }
        }

        /// <summary>
        ///     Called when [read completed].
        /// </summary>
        /// <param name="async">The asynchronous.</param>
        private void OnReadCompleted(IAsyncResult async)
        {
            try
            {
                Socket dataSocket = (Socket) async.AsyncState;

                if (_socket != null && _socket.Connected && _connected)
                {
                    int bytesReceived = dataSocket.EndReceive(async);

                    if (bytesReceived != 0)
                    {
                        byte[] array = new byte[bytesReceived];

                        Array.Copy(_buffer, array, bytesReceived);

                        HandlePacketData(array, bytesReceived);
                    }
                    else
                        Disconnect();
                }
            }
            catch (Exception exception)
            {
                HandleDisconnect(SocketError.ProtocolNotSupported, exception);
            }
            finally
            {
                try
                {
                    if (_socket != null && _socket.Connected && _connected)
                        _socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, OnReadCompleted, _socket);
                    else
                        Disconnect();
                }
                catch (Exception exception)
                {
                    HandleDisconnect(SocketError.ConnectionAborted, exception);
                }
            }
        }

        /// <summary>
        ///     Called when [send completed].
        /// </summary>
        /// <param name="async">The asynchronous.</param>
        private void OnSendCompleted(IAsyncResult async)
        {
            try
            {
                Socket dataSocket = (Socket) async.AsyncState;

                if (_socket != null && _socket.Connected && _connected)
                    dataSocket.EndSend(async);
                else
                    Disconnect();
            }
            catch (Exception exception)
            {
                HandleDisconnect(SocketError.ProtocolNotSupported, exception);
            }
        }

        /// <summary>
        ///     Cleanup everything so that the channel can be reused.
        /// </summary>
        public void Cleanup()
        {
            _socket = null;
            _connected = false;
        }

        /// <summary>
        ///     Starts the packet processing.
        /// </summary>
        public void StartPacketProcessing()
        {
            if (_connected && _socket.Connected)
                ReadAsync();
            else
                Dispose();
        }

        /// <summary>
        ///     Gets the ip.
        /// </summary>
        /// <returns>System.String.</returns>
        public string GetIp() => _remoteEndPoint.ToString().Split(':')[0];

        /// <summary>
        ///     Gets the connection identifier.
        /// </summary>
        /// <returns>System.UInt32.</returns>
        public uint GetConnectionId() => ChannelId;

        /// <summary>
        ///     Disconnects this instance.
        /// </summary>
        internal void Disconnect()
        {
            if (_connected)
                HandleDisconnect(SocketError.ConnectionReset, new SocketException((int) SocketError.ConnectionReset));
        }

        /// <summary>
        ///     Handles the packet data.
        /// </summary>
        /// <param name="packet">The packet.</param>
        /// <param name="bytesReceived"></param>
        private void HandlePacketData(byte[] packet, int bytesReceived)
        {
            if (Parser != null)
            {
                Arc4ServerSide?.Parse(ref packet);
                Parser.HandlePacketData(packet, bytesReceived);
            }
        }

        /// <summary>
        ///     Sends the data.
        /// </summary>
        /// <param name="packet">The packet.</param>
        public void SendData(byte[] packet)
        {
            if (_socket != null && _socket.Connected)
            {
                Arc4ClientSide?.Parse(ref packet);

                try
                {
                    _socket.BeginSend(packet, 0, packet.Length, SocketFlags.None, OnSendCompleted, _socket);
                }
                catch (Exception e)
                {
                    HandleDisconnect(SocketError.ConnectionReset, e);
                }
            }
            else
                Disconnect();
        }
    }
}