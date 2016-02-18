using System;
using System.Linq;
using Yupi.Emulator.Core.Io.Logger;
using Yupi.Emulator.Core.Security.BlackWords.Structs;
using Yupi.Emulator.Data.Base.Adapters.Interfaces;
using Yupi.Emulator.Game.Rooms.User;
using Yupi.Emulator.Game.Users;
using Yupi.Emulator.Game.Users.Data.Models;
using Yupi.Emulator.Game.Users.Factories;
using Yupi.Emulator.Messages;
using Yupi.Emulator.Messages.Buffers;
using Yupi.Emulator.Messages.Enums;
using Yupi.Emulator.Messages.Handlers;
using Yupi.Emulator.Net.Connection;

namespace Yupi.Emulator.Game.GameClients.Interfaces
{
    /// <summary>
    ///     Class GameClient.
    /// </summary>
    public class GameClient
    {
        /// <summary>
        ///     The Client Connection
        /// </summary>
        private ConnectionActor _connection;

        /// <summary>
        ///     The _habbo
        /// </summary>
        private Habbo _habbo;

        /// <summary>
        ///     The _message handler
        /// </summary>
        private GameClientMessageHandler _messageHandler;

        /// <summary>
        ///     The current room user identifier
        /// </summary>
        internal int CurrentRoomUserId;

        /// <summary>
        ///     The designed handler
        /// </summary>
        internal int DesignedHandler = 1;

        /// <summary>
        ///     The machine identifier
        /// </summary>
        internal string MachineId;

        /// <summary>
        ///     The publicist count
        /// </summary>
        internal byte PublicistCount;

        /// <summary>
        ///     The time pinged received
        /// </summary>
        internal DateTime TimePingedReceived;

        /// <summary>
        ///     Gets the connection identifier.
        /// </summary>
        /// <value>The connection identifier.</value>
        internal string ClientAddress;

        /// <summary>
        ///     Initializes a new instance of the <see cref="GameClient" /> class.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="connection">The connection.</param>
        internal GameClient(string clientId, ConnectionActor connection)
        {
            ClientAddress = clientId;

            _connection = connection;

            _connection.DataParser.SetConnection(_connection, this);

            CurrentRoomUserId = -1;
        }

        /// <summary>
        ///     Handles the publicist.
        /// </summary>
        /// <param name="word">The word.</param>
        /// <param name="message">The message.</param>
        /// <param name="method">The method.</param>
        /// <param name="settings">The settings.</param>
        internal void HandlePublicist(string word, string message, string method, BlackWordTypeSettings settings)
        {
            Habbo userPublicist = GetHabbo();

            if (userPublicist != null)
            {
                SimpleServerMessageBuffer simpleServerMessageBuffer;

                if (userPublicist.Rank < 5 && settings.MaxAdvices == PublicistCount++ && settings.AutoBan)
                {
                    simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingRequest("SuperNotificationMessageComposer"));
                    simpleServerMessageBuffer.AppendString("staffcloud");
                    simpleServerMessageBuffer.AppendInteger(2);
                    simpleServerMessageBuffer.AppendString("title");
                    simpleServerMessageBuffer.AppendString("Staff Internal Alert");
                    simpleServerMessageBuffer.AppendString("message");
                    simpleServerMessageBuffer.AppendString("O usuário " + userPublicist.UserName + " Fo Banido por Divulgar. A última palavra foi: " + word + ", na frase: " + message);

                    Yupi.GetGame().GetClientManager().StaffAlert(simpleServerMessageBuffer);

                    Yupi.GetGame().GetBanManager().BanUser(this, userPublicist.UserName, 788922000.0, "Você está divulgando Hoteis. Será banido para sempre..", true, true);

                    return;
                }

                string alert = settings.Alert.Replace("{0}", userPublicist.UserName);

                alert = alert.Replace("{1}", userPublicist.Id.ToString());
                alert = alert.Replace("{2}", word);
                alert = alert.Replace("{3}", message);
                alert = alert.Replace("{4}", method);

                simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingRequest("UsersClassificationMessageComposer"));
                simpleServerMessageBuffer.AppendInteger(1);
                simpleServerMessageBuffer.AppendInteger(userPublicist.Id);
                simpleServerMessageBuffer.AppendString(userPublicist.UserName);
                simpleServerMessageBuffer.AppendString("BadWord: " + word);

                Yupi.GetGame()?.GetClientManager()?.StaffAlert(simpleServerMessageBuffer);

                if (Yupi.GetGame().GetClientManager() == null)
                    return;

                foreach (GameClient client in Yupi.GetGame().GetClientManager().Clients.Values)
                {
                    if (client?.GetHabbo() == null)
                        continue;

                    if (client.CurrentRoomUserId == 0)
                        continue;

                    if (client.GetHabbo().Rank < 5)
                        continue;

                    simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingRequest("WhisperMessageComposer"));
                    simpleServerMessageBuffer.AppendInteger(client.CurrentRoomUserId);
                    simpleServerMessageBuffer.AppendString(alert);
                    simpleServerMessageBuffer.AppendInteger(0);
                    simpleServerMessageBuffer.AppendInteger(34);
                    simpleServerMessageBuffer.AppendInteger(0);
                    simpleServerMessageBuffer.AppendInteger(true);

                    client.SendMessage(simpleServerMessageBuffer);
                }
            }
        }

        /// <summary>
        ///     Gets the connection.
        /// </summary>
        /// <returns>ConnectionInformation.</returns>
        internal ConnectionActor GetConnection() => _connection;

        /// <summary>
        ///     Gets the message handler.
        /// </summary>
        /// <returns>GameClientMessageHandler.</returns>
        internal GameClientMessageHandler GetMessageHandler() => _messageHandler;

        /// <summary>
        ///     Gets the habbo.
        /// </summary>
        /// <returns>Habbo.</returns>
        internal Habbo GetHabbo() => _habbo;

        /// <summary>
        ///     Initializes the handler.
        /// </summary>
        internal void InitHandler()
        {
            _messageHandler = new GameClientMessageHandler(this);

            _connection.DataParser.SetConnection(_connection, this);

            _connection.HandShakeCompleted = true;

            TimePingedReceived = DateTime.Now;
        }

        /// <summary>
        ///     Tries the login.
        /// </summary>
        /// <param name="authTicket">The authentication ticket.</param>
        /// <param name="banReasonOut"></param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        internal bool TryLogin(string authTicket, out string banReasonOut)
        {
            banReasonOut = string.Empty;

            try
            {
                if (string.IsNullOrWhiteSpace(authTicket))
                    return false;

                string ip = GetConnection().ConnectionId;

                if (string.IsNullOrEmpty(ip))
                    return false;

                uint errorCode;

                UserData userData = UserDataFactory.GetUserData(authTicket, out errorCode);

                if (userData?.User == null)
                    return false;

                if (errorCode == 1 || errorCode == 2)
                    return false;

                Yupi.GetGame().GetClientManager().RegisterClient(this, userData.UserId, userData.User.UserName);

                _habbo = userData.User;

                userData.User.LoadData(userData);
 
                bool isBanned = Yupi.GetGame().GetBanManager().CheckIfIsBanned(userData.User.UserName, ip, MachineId);

                if (isBanned)
                {
                    string banReason = Yupi.GetGame().GetBanManager().GetBanReason(userData.User.UserName, ip, MachineId);

                    banReasonOut = banReason;

                    SendNotifWithScroll(banReason);

                    using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                    {
                        queryReactor.SetQuery($"SELECT ip_last FROM users WHERE id = {GetHabbo().Id} LIMIT 1");

                        string supaString = queryReactor.GetString();

                        queryReactor.SetQuery($"SELECT COUNT(0) FROM users_bans_access WHERE user_id={_habbo.Id} LIMIT 1");
                        int integer = queryReactor.GetInteger();

                        if (integer > 0)
                            queryReactor.RunFastQuery("UPDATE users_bans_access SET attempts = attempts + 1, ip='" + supaString + "' WHERE user_id=" + GetHabbo().Id + " LIMIT 1");
                        else
                            queryReactor.RunFastQuery("INSERT INTO users_bans_access (user_id, ip) VALUES (" + GetHabbo().Id + ", '" + supaString + "')");
                    }

                    return false;
                }

                using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                    queryReactor.RunFastQuery($"UPDATE users SET ip_last='{ip}' WHERE id={GetHabbo().Id}");

                userData.User.Init(this, userData);

                QueuedServerMessageBuffer queuedServerMessageBuffer = new QueuedServerMessageBuffer(_connection);

                SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingRequest("UniqueMachineIDMessageComposer"));

                simpleServerMessageBuffer.AppendString(MachineId);
                queuedServerMessageBuffer.AppendResponse(simpleServerMessageBuffer);

                queuedServerMessageBuffer.AppendResponse(
                    new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingRequest("AuthenticationOKMessageComposer")));

                SimpleServerMessageBuffer simpleServerMessage2 = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingRequest("HomeRoomMessageComposer"));

                simpleServerMessage2.AppendInteger(_habbo.HomeRoom);
                simpleServerMessage2.AppendInteger(_habbo.HomeRoom);
                queuedServerMessageBuffer.AppendResponse(simpleServerMessage2);

                simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingRequest("MinimailCountMessageComposer"));

                simpleServerMessageBuffer.AppendInteger(_habbo.MinimailUnreadMessages);
                queuedServerMessageBuffer.AppendResponse(simpleServerMessageBuffer);

                simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingRequest("FavouriteRoomsMessageComposer"));

                simpleServerMessageBuffer.AppendInteger(30);

                if (userData.User.FavoriteRooms == null || !userData.User.FavoriteRooms.Any())
                    simpleServerMessageBuffer.AppendInteger(0);
                else
                {
                    simpleServerMessageBuffer.AppendInteger(userData.User.FavoriteRooms.Count);

                    foreach (uint i in userData.User.FavoriteRooms)
                        simpleServerMessageBuffer.AppendInteger(i);
                }

                queuedServerMessageBuffer.AppendResponse(simpleServerMessageBuffer);

                SimpleServerMessageBuffer rightsMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingRequest("UserClubRightsMessageComposer"));

                rightsMessageBuffer.AppendInteger(userData.User.GetSubscriptionManager().HasSubscription ? 2 : 0);
                rightsMessageBuffer.AppendInteger(userData.User.Rank);
                rightsMessageBuffer.AppendInteger(0);
                queuedServerMessageBuffer.AppendResponse(rightsMessageBuffer);

                simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingRequest("EnableNotificationsMessageComposer"));
                simpleServerMessageBuffer.AppendBool(true); //isOpen
                simpleServerMessageBuffer.AppendBool(false);
                queuedServerMessageBuffer.AppendResponse(simpleServerMessageBuffer);

                simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingRequest("EnableTradingMessageComposer"));
                simpleServerMessageBuffer.AppendBool(true);
                queuedServerMessageBuffer.AppendResponse(simpleServerMessageBuffer);
                userData.User.UpdateCreditsBalance();

                simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingRequest("ActivityPointsMessageComposer"));
                simpleServerMessageBuffer.AppendInteger(2);
                simpleServerMessageBuffer.AppendInteger(0);
                simpleServerMessageBuffer.AppendInteger(userData.User.Duckets);
                simpleServerMessageBuffer.AppendInteger(5);
                simpleServerMessageBuffer.AppendInteger(userData.User.Diamonds);
                queuedServerMessageBuffer.AppendResponse(simpleServerMessageBuffer);

                if (userData.User.HasFuse("fuse_mod"))
                    queuedServerMessageBuffer.AppendResponse(Yupi.GetGame().GetModerationTool().SerializeTool(this));

                queuedServerMessageBuffer.AppendResponse(Yupi.GetGame().GetAchievementManager().AchievementDataCached);

                queuedServerMessageBuffer.AppendResponse(GetHabbo().GetAvatarEffectsInventoryComponent().GetPacket());
                queuedServerMessageBuffer.SendResponse();

                Yupi.GetGame().GetAchievementManager().TryProgressHabboClubAchievements(this);
                Yupi.GetGame().GetAchievementManager().TryProgressRegistrationAchievements(this);
                Yupi.GetGame().GetAchievementManager().TryProgressLoginAchievements(this);

                return true;
            }
            catch (Exception ex)
            {
                YupiLogManager.LogException(ex, "Registered Login Exception.", "Yupi.User");
            }

            return false;
        }

        /// <summary>
        ///     Sends the notif with scroll.
        /// </summary>
        /// <param name="message">The message.</param>
        internal void SendNotifWithScroll(string message)
        {
            SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingRequest("MOTDNotificationMessageComposer"));

            simpleServerMessageBuffer.AppendInteger(1);
            simpleServerMessageBuffer.AppendString(message);
            SendMessage(simpleServerMessageBuffer);
        }

        /// <summary>
        ///     Sends the broadcast message.
        /// </summary>
        /// <param name="message">The message.</param>
        internal void SendBroadcastMessage(string message)
        {
            SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingRequest("BroadcastNotifMessageComposer"));

            simpleServerMessageBuffer.AppendString(message);
            simpleServerMessageBuffer.AppendString(string.Empty);
            SendMessage(simpleServerMessageBuffer);
        }

        /// <summary>
        ///     Sends the moderator message.
        /// </summary>
        /// <param name="message">The message.</param>
        internal void SendModeratorMessage(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                return;

            SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingRequest("AlertNotificationMessageComposer"));

            simpleServerMessageBuffer.AppendString(message);
            simpleServerMessageBuffer.AppendString(string.Empty);

            SendMessage(simpleServerMessageBuffer);
        }

        /// <summary>
        ///     Sends the whisper.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="fromWired"></param>
        internal void SendWhisper(string message, bool fromWired = false)
        {
            if (GetHabbo() == null || GetHabbo().CurrentRoom == null)
                return;

            RoomUser roomUserByHabbo = GetHabbo().CurrentRoom.GetRoomUserManager().GetRoomUserByHabbo(GetHabbo().UserName);

            if (roomUserByHabbo == null)
                return;

            SimpleServerMessageBuffer whisp = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingRequest("WhisperMessageComposer"));

            whisp.AppendInteger(roomUserByHabbo.VirtualId);
            whisp.AppendString(message);
            whisp.AppendInteger(0);
            whisp.AppendInteger(fromWired ? 34 : roomUserByHabbo.LastBubble);
            whisp.AppendInteger(0);
            whisp.AppendInteger(fromWired);

            SendMessage(whisp);
        }

        /// <summary>
        ///     Sends the notif.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="title">The title.</param>
        /// <param name="picture">The picture.</param>
        internal void SendNotif(string message, string title = "Aviso", string picture = "") => SendMessage(GetBytesNotif(message, title, picture));

        /// <summary>
        ///     Gets the bytes notif.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="title">The title.</param>
        /// <param name="picture">The picture.</param>
        /// <returns>System.Byte[].</returns>
        public static byte[] GetBytesNotif(string message, string title = "Aviso", string picture = "")
        {
            using (SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingRequest("SuperNotificationMessageComposer")))
            {
                simpleServerMessageBuffer.AppendString(picture);
                simpleServerMessageBuffer.AppendInteger(4);
                simpleServerMessageBuffer.AppendString("title");
                simpleServerMessageBuffer.AppendString(title);
                simpleServerMessageBuffer.AppendString("message");
                simpleServerMessageBuffer.AppendString(message);
                simpleServerMessageBuffer.AppendString("linkUrl");
                simpleServerMessageBuffer.AppendString("event:");
                simpleServerMessageBuffer.AppendString("linkTitle");
                simpleServerMessageBuffer.AppendString("ok");

                return simpleServerMessageBuffer.GetReversedBytes();
            }
        }

        /// <summary>
        ///     Disconnects the specified reason.
        /// </summary>
        /// <param name="reason">The reason.</param>
        /// <param name="showConsole"></param>
        internal void Disconnect(string reason = "Left Game", bool showConsole = false)
        {
            if (GetConnection().HandShakeCompleted)
            {
                GetHabbo()?.RunDbUpdate();

                GetHabbo()?.OnDisconnect(reason, showConsole);

                GetMessageHandler()?.Destroy();
            }

            GetConnection()?.Close();

            CurrentRoomUserId = -1;

            _messageHandler = null;

            _habbo = null;

            _connection = null;
        }

        /// <summary>
        ///     Sends the message.
        /// </summary>
        /// <param name="message">The message.</param>
        internal void SendMessage(SimpleServerMessageBuffer message)
        {
            if (message == null)
                return;

            if (GetConnection() == null)
                return;

            byte[] bytes = message.GetReversedBytes();

            GetConnection().ConnectionChannel.WriteAsync(bytes);
        }

        /// <summary>
        ///     Sends the message.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        internal void SendMessage(byte[] bytes)
        {
            if (GetConnection() == null)
                return;

            GetConnection().ConnectionChannel.WriteAsync(bytes);
        }

        /// <summary>
        ///     Sends the message.
        /// </summary>
        /// <param name="type">The type.</param>
        internal void SendMessage(StaticMessage type)
        {
            if (GetConnection() == null)
                return;

            GetConnection().ConnectionChannel.WriteAsync(StaticMessagesManager.Get(type));
        }
    }
}