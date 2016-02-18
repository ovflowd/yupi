using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace Azure.Connection.Net
{
    public class MusSocket
    {
        internal Socket MsSocket;
        internal string MusIp;
        internal int MusPort;
        internal HashSet<string> AllowedIps;

        internal MusSocket(string musIp, int musPort, IEnumerable<string> allowedIps, int backlog)
        {
            MusIp = musIp;
            MusPort = musPort;
            AllowedIps = new HashSet<string>();
            foreach (var item in allowedIps) AllowedIps.Add(item);
            try
            {
                Out.WriteLine(
                    "Starting up asynchronous sockets server for MUS connections for port " +
                    musPort, "Server.AsyncSocketMusListener");

                MsSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                MsSocket.Bind(new IPEndPoint(IPAddress.Any, MusPort));
                MsSocket.Listen(backlog);
                MsSocket.BeginAccept(OnEvent_NewConnection, MsSocket);

                Out.WriteLine(
                    "Asynchronous sockets server for MUS connections running on port " +
                    musPort + Environment.NewLine,
                    "Server.AsyncSocketMusListener");
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"No se pudo iniciar el Socket MUS:\n{ex}");
            }
        }

        internal void OnEvent_NewConnection(IAsyncResult iAr)
        {
            try
            {
                var socket = ((Socket)iAr.AsyncState).EndAccept(iAr);
                var ip = socket.RemoteEndPoint.ToString().Split(':')[0];

                if (AllowedIps.Contains(ip) || ip == "127.0.0.1")
                    new MusConnection(socket);
                else
                    socket.Close();
            }
            catch (Exception e)
            {
                Writer.Writer.LogException(e.ToString());
            }

            MsSocket.BeginAccept(OnEvent_NewConnection, MsSocket);
        }
    }
}