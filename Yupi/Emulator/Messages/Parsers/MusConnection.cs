using System;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using Azure.HabboHotel.GameClients.Interfaces;

namespace Azure.Connection.Net
{
    internal class MusConnection
    {
        private Socket _socket;
        private byte[] _buffer = new byte[1024];

        internal MusConnection(Socket socket)
        {
            _socket = socket;

            try
            {
                _socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, OnEvent_RecieveData, _socket);
            }
            catch
            {
                TryClose();
            }
        }

        internal void TryClose()
        {
            try
            {
                if (_socket == null)
                    return;

                _socket.Shutdown(SocketShutdown.Both);

                _socket.Close();
                _socket.Dispose();
            }
            catch
            {
                // ignored
            }

            _socket = null;
            _buffer = null;
        }

        internal void OnEvent_RecieveData(IAsyncResult iAr)
        {
            try
            {
                int lenght;

                try
                {
                    lenght = _socket.EndReceive(iAr);
                }
                catch
                {
                    TryClose();
                    return;
                }

                var theString = Encoding.Default.GetString(_buffer, 0, lenght);

                if (theString.Contains(((char)0).ToString()))
                    foreach (var str in theString.Split((char)0))
                        ProcessCommand(@str);
            }
            catch (Exception value)
            {
                Writer.Writer.LogException(value.ToString());
            }
            TryClose();
        }

        internal void ProcessCommand(string data)
        {
            if (!data.Contains(((char)1).ToString())) return;

            var parts = data.Split((char)1);
            var header = parts[1].ToLower();
            if (header == string.Empty) return;
            parts = parts.Skip(1).ToArray();

            GameClient clientByUserId;
            uint userId;
            switch (header)
            {
                case "addtoinventory":
                    userId = Convert.ToUInt32(parts[0]);
                    var furniId = Convert.ToInt32(parts[1]);

                    clientByUserId = Azure.GetGame().GetClientManager().GetClientByUserId(userId);
                    if (clientByUserId == null || clientByUserId.GetHabbo() == null ||
                        clientByUserId.GetHabbo().GetInventoryComponent() == null)
                        return;

                    clientByUserId.GetHabbo().GetInventoryComponent().UpdateItems(true);
                    clientByUserId.GetHabbo().GetInventoryComponent().SendNewItems((uint)furniId);

                    break;

                case "updatecredits":
                    userId = Convert.ToUInt32(parts[0]);
                    var credits = Convert.ToInt32(parts[1]);

                    clientByUserId = Azure.GetGame().GetClientManager().GetClientByUserId(userId);
                    if (clientByUserId != null && clientByUserId.GetHabbo() != null)
                    {
                        clientByUserId.GetHabbo().Credits = credits;
                        clientByUserId.GetHabbo().UpdateCreditsBalance();
                    }
                    return;

                case "updatesubscription":
                    userId = Convert.ToUInt32(parts[0]);

                    clientByUserId = Azure.GetGame().GetClientManager().GetClientByUserId(userId);
                    if (clientByUserId == null || clientByUserId.GetHabbo() == null) return;
                    clientByUserId.GetHabbo().GetSubscriptionManager().ReloadSubscription();
                    clientByUserId.GetHabbo().SerializeClub();
                    break;
            }
        }
    }
}