using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using Yupi.Data.Base.Adapters.Interfaces;
using Yupi.Game.GameClients.Interfaces;
using Yupi.Game.Rooms;
using Yupi.Game.Rooms.Chat;
using Yupi.Game.Rooms.Data;
using Yupi.Game.Users;
using Yupi.Messages;
using Yupi.Messages.Parsers;

namespace Yupi.Game.Support
{
    /// <summary>
    ///     Class ModerationTool.
    /// </summary>
    public class ModerationTool
    {
        /// <summary>
        ///     Abusive suppot ticket cooldown
        /// </summary>
        internal Dictionary<uint, double> AbusiveCooldown;

        /// <summary>
        ///     The moderation templates
        /// </summary>
        internal Dictionary<uint, ModerationTemplate> ModerationTemplates;

        /// <summary>
        ///     The room message presets
        /// </summary>
        internal List<string> RoomMessagePresets;

        /// <summary>
        ///     The support ticket hints
        /// </summary>
        internal StringDictionary SupportTicketHints;

        /// <summary>
        ///     The tickets
        /// </summary>
        internal List<SupportTicket> Tickets;

        /// <summary>
        ///     The user message presets
        /// </summary>
        internal List<string> UserMessagePresets;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ModerationTool" /> class.
        /// </summary>
        internal ModerationTool()
        {
            Tickets = new List<SupportTicket>();
            UserMessagePresets = new List<string>();
            RoomMessagePresets = new List<string>();
            SupportTicketHints = new StringDictionary();
            ModerationTemplates = new Dictionary<uint, ModerationTemplate>();
            AbusiveCooldown = new Dictionary<uint, double>();
        }

        /// <summary>
        ///     Sends the ticket update to moderators.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        internal static void SendTicketUpdateToModerators(SupportTicket ticket)
        {
        }

        /// <summary>
        ///     Sends the ticket to moderators.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        internal static void SendTicketToModerators(SupportTicket ticket)
        {
            ServerMessage message = new ServerMessage(LibraryParser.OutgoingRequest("ModerationToolIssueMessageComposer"));
            message = ticket.Serialize(message);

            Yupi.GetGame().GetClientManager().StaffAlert(message);
        }

        /// <summary>
        ///     Performs the room action.
        /// </summary>
        /// <param name="modSession">The mod session.</param>
        /// <param name="roomId">The room identifier.</param>
        /// <param name="kickUsers">if set to <c>true</c> [kick users].</param>
        /// <param name="lockRoom">if set to <c>true</c> [lock room].</param>
        /// <param name="inappropriateRoom">if set to <c>true</c> [inappropriate room].</param>
        /// <param name="message">The message.</param>
        internal static void PerformRoomAction(GameClient modSession, uint roomId, bool kickUsers, bool lockRoom,
            bool inappropriateRoom, ServerMessage message)
        {
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(roomId);

            if (room == null)
                return;

            if (lockRoom)
            {
                room.RoomData.State = 1;

                using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                    commitableQueryReactor.RunFastQuery(
                        $"UPDATE rooms_data SET state = 'locked' WHERE id = {room.RoomId}");
            }

            if (inappropriateRoom)
            {
                room.RoomData.Name = "Inapropriado para a Gerência do Hotel";
                room.RoomData.Description = "A descrição do quarto não é permitida.";
                room.ClearTags();
                room.RoomData.SerializeRoomData(message, modSession, false, true);
            }

            if (kickUsers)
                room.OnRoomKick();
        }

        /// <summary>
        ///     Mods the action result.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="result">if set to <c>true</c> [result].</param>
        internal static void ModActionResult(uint userId, bool result)
        {
            GameClient clientByUserId = Yupi.GetGame().GetClientManager().GetClientByUserId(userId);
            clientByUserId.GetMessageHandler()
                .GetResponse()
                .Init(LibraryParser.OutgoingRequest("ModerationActionResultMessageComposer"));
            clientByUserId.GetMessageHandler().GetResponse().AppendInteger(userId);
            clientByUserId.GetMessageHandler().GetResponse().AppendBool(false);
            clientByUserId.GetMessageHandler().SendResponse();
        }

        /// <summary>
        ///     Serializes the room tool.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>ServerMessage.</returns>
        internal static ServerMessage SerializeRoomTool(RoomData data)
        {
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(data.Id);

            ServerMessage serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("ModerationRoomToolMessageComposer"));
            serverMessage.AppendInteger(data.Id);
            serverMessage.AppendInteger(data.UsersNow);

            if (room != null)
                serverMessage.AppendBool(room.GetRoomUserManager().GetRoomUserByHabbo(data.Owner) != null);
            else
                serverMessage.AppendBool(false);

            serverMessage.AppendInteger(room?.RoomData.OwnerId ?? 0);
            serverMessage.AppendString(data.Owner);
            serverMessage.AppendBool(room != null);
            serverMessage.AppendString(data.Name);
            serverMessage.AppendString(data.Description);
            serverMessage.AppendInteger(data.TagCount);

            foreach (string current in data.Tags)
                serverMessage.AppendString(current);

            serverMessage.AppendBool(false);

            return serverMessage;
        }

        /// <summary>
        ///     Kicks the user.
        /// </summary>
        /// <param name="modSession">The mod session.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="message">The message.</param>
        /// <param name="soft">if set to <c>true</c> [soft].</param>
        internal static void KickUser(GameClient modSession, uint userId, string message, bool soft)
        {
            GameClient clientByUserId = Yupi.GetGame().GetClientManager().GetClientByUserId(userId);

            if (clientByUserId == null || clientByUserId.GetHabbo().CurrentRoomId < 1 ||
                clientByUserId.GetHabbo().Id == modSession.GetHabbo().Id)
            {
                ModActionResult(modSession.GetHabbo().Id, false);
                return;
            }

            if (clientByUserId.GetHabbo().Rank >= modSession.GetHabbo().Rank)
            {
                ModActionResult(modSession.GetHabbo().Id, false);
                return;
            }

            Room room = Yupi.GetGame().GetRoomManager().GetRoom(clientByUserId.GetHabbo().CurrentRoomId);

            if (room == null)
                return;

            room.GetRoomUserManager().RemoveUserFromRoom(clientByUserId, true, false);
            clientByUserId.CurrentRoomUserId = -1;

            clientByUserId.SendNotif(message);

            if (soft)
                return;

            using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                commitableQueryReactor.RunFastQuery(
                    $"UPDATE users_info SET cautions = cautions + 1 WHERE user_id = {userId}");
        }

        /// <summary>
        ///     Alerts the user.
        /// </summary>
        /// <param name="modSession">The mod session.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="message">The message.</param>
        /// <param name="caution">if set to <c>true</c> [caution].</param>
        internal static void AlertUser(GameClient modSession, uint userId, string message, bool caution)
        {
            GameClient clientByUserId = Yupi.GetGame().GetClientManager().GetClientByUserId(userId);

            clientByUserId?.SendModeratorMessage(message);
        }

        /// <summary>
        ///     Locks the trade.
        /// </summary>
        /// <param name="modSession">The mod session.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="message">The message.</param>
        /// <param name="length">The length.</param>
        internal static void LockTrade(GameClient modSession, uint userId, string message, int length)
        {
            GameClient clientByUserId = Yupi.GetGame().GetClientManager().GetClientByUserId(userId);

            if (clientByUserId == null)
                return;

            if (!clientByUserId.GetHabbo().CheckTrading())
                length += Yupi.GetUnixTimeStamp() - clientByUserId.GetHabbo().TradeLockExpire;

            clientByUserId.GetHabbo().TradeLocked = true;
            clientByUserId.GetHabbo().TradeLockExpire = Yupi.GetUnixTimeStamp() + length;
            clientByUserId.SendNotif(message);

            using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                commitableQueryReactor.RunFastQuery(
                    $"UPDATE users SET trade_lock = '1', trade_lock_expire = '{clientByUserId.GetHabbo().TradeLockExpire}' WHERE id = '{clientByUserId.GetHabbo().Id}'");
        }

        /// <summary>
        ///     Bans the user.
        /// </summary>
        /// <param name="modSession">The mod session.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="length">The length.</param>
        /// <param name="message">The message.</param>
        internal static void BanUser(GameClient modSession, uint userId, int length, string message)
        {
            GameClient clientByUserId = Yupi.GetGame().GetClientManager().GetClientByUserId(userId);

            if (clientByUserId == null || clientByUserId.GetHabbo().Id == modSession.GetHabbo().Id)
            {
                ModActionResult(modSession.GetHabbo().Id, false);
                return;
            }

            if (clientByUserId.GetHabbo().Rank >= modSession.GetHabbo().Rank)
            {
                ModActionResult(modSession.GetHabbo().Id, false);
                return;
            }

            Yupi.GetGame()
                .GetBanManager()
                .BanUser(clientByUserId, modSession.GetHabbo().UserName, length, message, false, false);
        }

        /// <summary>
        ///     Serializes the user information.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>ServerMessage.</returns>
        /// <exception cref="System.NullReferenceException">User not found in database.</exception>
        internal static ServerMessage SerializeUserInfo(uint userId)
        {
            ServerMessage serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("ModerationToolUserToolMessageComposer"));
            using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                if (commitableQueryReactor != null)
                {
                    commitableQueryReactor.SetQuery(
                        "SELECT id, username, mail, look, trade_lock, trade_lock_expire, rank, ip_last, " +
                        "IFNULL(cfhs, 0) cfhs, IFNULL(cfhs_abusive, 0) cfhs_abusive, IFNULL(cautions, 0) cautions, IFNULL(bans, 0) bans, " +
                        "IFNULL(reg_timestamp, 0) reg_timestamp, IFNULL(login_timestamp, 0) login_timestamp " +
                        $"FROM users left join users_info on (users.id = users_info.user_id) WHERE id = '{userId}' LIMIT 1"
                        );

                    DataRow row = commitableQueryReactor.GetRow();

                    uint id = Convert.ToUInt32(row["id"]);
                    serverMessage.AppendInteger(id);
                    serverMessage.AppendString(row["username"].ToString());
                    serverMessage.AppendString(row["look"].ToString());
                    double regTimestamp = (double) row["reg_timestamp"];
                    double loginTimestamp = (double) row["login_timestamp"];
                    int unixTimestamp = Yupi.GetUnixTimeStamp();
                    serverMessage.AppendInteger(
                        (int) (regTimestamp > 0 ? Math.Ceiling((unixTimestamp - regTimestamp)/60.0) : regTimestamp));
                    serverMessage.AppendInteger(
                        (int)
                            (loginTimestamp > 0 ? Math.Ceiling((unixTimestamp - loginTimestamp)/60.0) : loginTimestamp));
                    serverMessage.AppendBool(true);
                    serverMessage.AppendInteger(Convert.ToInt32(row["cfhs"]));
                    serverMessage.AppendInteger(Convert.ToInt32(row["cfhs_abusive"]));
                    serverMessage.AppendInteger(Convert.ToInt32(row["cautions"]));
                    serverMessage.AppendInteger(Convert.ToInt32(row["bans"]));

                    serverMessage.AppendInteger(0);
                    uint rank = (uint) row["rank"];
                    serverMessage.AppendString(row["trade_lock"].ToString() == "1"
                        ? Yupi.UnixToDateTime(int.Parse(row["trade_lock_expire"].ToString())).ToLongDateString()
                        : "Not trade-locked");
                    serverMessage.AppendString(rank < 6 ? row["ip_last"].ToString() : "127.0.0.1");
                    serverMessage.AppendInteger(id);
                    serverMessage.AppendInteger(0);

                    serverMessage.AppendString($"E-Mail:         {row["mail"]}");
                    serverMessage.AppendString($"Rank ID:        {rank}");
                }
            }
            return serverMessage;
        }

        /// <summary>
        ///     Serializes the room visits.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>ServerMessage.</returns>
        internal static ServerMessage SerializeRoomVisits(uint userId)
        {
            ServerMessage serverMessage =
                new ServerMessage(LibraryParser.OutgoingRequest("ModerationToolRoomVisitsMessageComposer"));
            serverMessage.AppendInteger(userId);

            GameClient user = Yupi.GetGame().GetClientManager().GetClientByUserId(userId);

            if (user?.GetHabbo() == null)
            {
                serverMessage.AppendString("Not online");
                serverMessage.AppendInteger(0);
                return serverMessage;
            }

            serverMessage.AppendString(user.GetHabbo().UserName);
            serverMessage.StartArray();

            foreach (
                RoomData roomData in
                    user.GetHabbo()
                        .RecentlyVisitedRooms.Select(roomId => Yupi.GetGame().GetRoomManager().GenerateRoomData(roomId))
                        .Where(roomData => roomData != null))
            {
                serverMessage.AppendInteger(roomData.Id);
                serverMessage.AppendString(roomData.Name);

                serverMessage.AppendInteger(0); //hour
                serverMessage.AppendInteger(0); //min

                serverMessage.SaveArray();
            }

            serverMessage.EndArray();
            return serverMessage;
        }

        /// <summary>
        ///     Serializes the user chatlog.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>ServerMessage.</returns>
        internal static ServerMessage SerializeUserChatlog(uint userId)
        {
            ServerMessage result;

            using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                commitableQueryReactor.SetQuery($"SELECT DISTINCT room_id FROM users_chatlogs WHERE user_id = '{userId}' ORDER BY timestamp DESC LIMIT 4");

                DataTable table = commitableQueryReactor.GetTable();

                ServerMessage serverMessage =  new ServerMessage(LibraryParser.OutgoingRequest("ModerationToolUserChatlogMessageComposer"));
                serverMessage.AppendInteger(userId);
                serverMessage.AppendString(Yupi.GetGame().GetClientManager().GetNameById(userId));

                if (table != null)
                {
                    serverMessage.AppendInteger(table.Rows.Count);
                    IEnumerator enumerator = table.Rows.GetEnumerator();

                    try
                    {
                        while (enumerator.MoveNext())
                        {
                            DataRow dataRow = (DataRow) enumerator.Current;

                            commitableQueryReactor.SetQuery($"SELECT user_id,timestamp,message FROM users_chatlogs WHERE room_id = {dataRow["room_id"]} AND user_id = {userId} ORDER BY timestamp DESC LIMIT 30");

                            DataTable table2 = commitableQueryReactor.GetTable();
                            RoomData roomData = Yupi.GetGame().GetRoomManager().GenerateRoomData((uint) dataRow["room_id"]);

                            if (table2 != null)
                            {
                                serverMessage.AppendByte(1);
                                serverMessage.AppendShort(2);
                                serverMessage.AppendString("roomName");
                                serverMessage.AppendByte(2);
                                serverMessage.AppendString(roomData == null ? "This room was deleted" : roomData.Name);
                                serverMessage.AppendString("roomId");
                                serverMessage.AppendByte(1);
                                serverMessage.AppendInteger((uint) dataRow["room_id"]);
                                serverMessage.AppendShort(table2.Rows.Count);
                                IEnumerator enumerator2 = table2.Rows.GetEnumerator();
                                try
                                {
                                    while (enumerator2.MoveNext())
                                    {
                                        DataRow dataRow2 = (DataRow) enumerator2.Current;

                                        Habbo habboForId = Yupi.GetHabboById((uint) dataRow2["user_id"]);
                                        Yupi.UnixToDateTime((double) dataRow2["timestamp"]);

                                        if (habboForId == null)
                                            return null;

                                        serverMessage.AppendInteger(
                                            (int) (Yupi.GetUnixTimeStamp() - (double) dataRow2["timestamp"]));

                                        serverMessage.AppendInteger(habboForId.Id);
                                        serverMessage.AppendString(habboForId.UserName);
                                        serverMessage.AppendString(dataRow2["message"].ToString());
                                        serverMessage.AppendBool(false);
                                    }
                                    continue;
                                }
                                finally
                                {
                                    IDisposable disposable = enumerator2 as IDisposable;

                                    disposable?.Dispose();
                                }
                            }

                            serverMessage.AppendByte(1);
                            serverMessage.AppendShort(0);
                            serverMessage.AppendShort(0);
                        }

                        result = serverMessage;
                        return result;
                    }
                    finally
                    {
                        IDisposable disposable2 = enumerator as IDisposable;

                        disposable2?.Dispose();
                    }
                }

                serverMessage.AppendInteger(0);
                result = serverMessage;
            }
            return result;
        }

        /// <summary>
        ///     Serializes the ticket chatlog.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="roomData">The room data.</param>
        /// <param name="timestamp">The timestamp.</param>
        /// <returns>ServerMessage.</returns>
        /// <exception cref="System.NullReferenceException">No room found.</exception>
        internal static ServerMessage SerializeTicketChatlog(SupportTicket ticket, RoomData roomData, double timestamp)
        {
            ServerMessage message = new ServerMessage();

            RoomData room = Yupi.GetGame().GetRoomManager().GenerateRoomData(ticket.RoomId);

            if (room != null)
            {
                message.Init(LibraryParser.OutgoingRequest("ModerationToolIssueChatlogMessageComposer"));

                message.AppendInteger(ticket.TicketId);
                message.AppendInteger(ticket.SenderId);
                message.AppendInteger(ticket.ReportedId);
                message.AppendInteger(ticket.RoomId);

                message.AppendByte(1);
                message.AppendShort(2);
                message.AppendString("roomName");
                message.AppendByte(2);
                message.AppendString(ticket.RoomName);
                message.AppendString("roomId");
                message.AppendByte(1);
                message.AppendInteger(ticket.RoomId);

                List<Chatlog> tempChatlogs =
                    room.RoomChat.Reverse().Skip(Math.Max(0, room.RoomChat.Count() - 60)).Take(60).ToList();

                message.AppendShort(tempChatlogs.Count);

                foreach (Chatlog chatLog in tempChatlogs)
                    chatLog.Serialize(ref message);

                return message;
            }
            return null;
        }

        /// <summary>
        ///     Serializes the room chatlog.
        /// </summary>
        /// <param name="roomId">The room identifier.</param>
        /// <returns>ServerMessage.</returns>
        /// <exception cref="System.NullReferenceException">No room found.</exception>
        internal static ServerMessage SerializeRoomChatlog(uint roomId)
        {
            ServerMessage message = new ServerMessage();

            Room room = Yupi.GetGame().GetRoomManager().LoadRoom(roomId);

            if (room?.RoomData != null)
            {
                message.Init(LibraryParser.OutgoingRequest("ModerationToolRoomChatlogMessageComposer"));
                message.AppendByte(1);
                message.AppendShort(2);
                message.AppendString("roomName");
                message.AppendByte(2);
                message.AppendString(room.RoomData.Name);
                message.AppendString("roomId");
                message.AppendByte(1);
                message.AppendInteger(room.RoomData.Id);

                List<Chatlog> tempChatlogs =
                    room.RoomData.RoomChat.Reverse()
                        .Skip(Math.Max(0, room.RoomData.RoomChat.Count - 60))
                        .Take(60)
                        .ToList();

                message.AppendShort(tempChatlogs.Count);

                foreach (Chatlog chatLog in tempChatlogs)
                    chatLog.Serialize(ref message);

                return message;
            }

            return null;
        }

        /// <summary>
        ///     Serializes the tool.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <returns>ServerMessage.</returns>
        internal ServerMessage SerializeTool(GameClient session)
        {
            ServerMessage serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("LoadModerationToolMessageComposer"));

            serverMessage.AppendInteger(Tickets.Count);

            foreach (SupportTicket current in Tickets)
                current.Serialize(serverMessage);

            serverMessage.AppendInteger(UserMessagePresets.Count);

            foreach (string current2 in UserMessagePresets)
                serverMessage.AppendString(current2);

            IEnumerable<ModerationTemplate> enumerable =
                (from x in ModerationTemplates.Values where x.Category == -1 select x).ToArray();

            serverMessage.AppendInteger(enumerable.Count());
            using (IEnumerator<ModerationTemplate> enumerator3 = enumerable.GetEnumerator())
            {
                bool first = true;

                while (enumerator3.MoveNext())
                {
                    ModerationTemplate template = enumerator3.Current;
                    IEnumerable<ModerationTemplate> enumerable2 =
                        (from x in ModerationTemplates.Values where x.Category == (long) (ulong) template.Id select x)
                            .ToArray();
                    serverMessage.AppendString(template.CName);
                    serverMessage.AppendBool(first);
                    serverMessage.AppendInteger(enumerable2.Count());

                    foreach (ModerationTemplate current3 in enumerable2)
                    {
                        serverMessage.AppendString(current3.Caption);
                        serverMessage.AppendString(current3.BanMessage);
                        serverMessage.AppendInteger(current3.BanHours);
                        serverMessage.AppendInteger(Yupi.BoolToInteger(current3.AvatarBan));
                        serverMessage.AppendInteger(Yupi.BoolToInteger(current3.Mute));
                        serverMessage.AppendInteger(Yupi.BoolToInteger(current3.TradeLock));
                        serverMessage.AppendString(current3.WarningMessage);
                        serverMessage.AppendBool(true);
                    }

                    first = false;
                }
            }

            // but = button
            serverMessage.AppendBool(true); //ticket_queue_but
            serverMessage.AppendBool(session.GetHabbo().HasFuse("fuse_chatlogs")); //chatlog_but
            serverMessage.AppendBool(session.GetHabbo().HasFuse("fuse_alert")); //message_but
            serverMessage.AppendBool(true); //modaction_but
            serverMessage.AppendBool(session.GetHabbo().HasFuse("fuse_ban")); //ban_but
            serverMessage.AppendBool(true);
            serverMessage.AppendBool(session.GetHabbo().HasFuse("fuse_kick")); //kick_but

            serverMessage.AppendInteger(RoomMessagePresets.Count);

            foreach (string current4 in RoomMessagePresets)
                serverMessage.AppendString(current4);

            return serverMessage;
        }

        /// <summary>
        ///     Loads the message presets.
        /// </summary>
        /// <param name="dbClient">The database client.</param>
        internal void LoadMessagePresets(IQueryAdapter dbClient)
        {
            UserMessagePresets.Clear();
            RoomMessagePresets.Clear();
            SupportTicketHints.Clear();
            ModerationTemplates.Clear();
            dbClient.SetQuery("SELECT type,message FROM moderation_presets WHERE enabled = 2");
            DataTable table = dbClient.GetTable();
            dbClient.SetQuery("SELECT word,hint FROM moderation_tickethints");
            DataTable table2 = dbClient.GetTable();
            dbClient.SetQuery("SELECT * FROM moderation_templates");
            DataTable table3 = dbClient.GetTable();

            if (table == null || table2 == null)
                return;

            foreach (DataRow dataRow in table.Rows)
            {
                string item = (string) dataRow["message"];
                string a = dataRow["type"].ToString().ToLower();

                if (a != "message")
                {
                    switch (a)
                    {
                        case "roommessage":
                            RoomMessagePresets.Add(item);
                            break;
                    }
                }
                else
                    UserMessagePresets.Add(item);
            }

            foreach (DataRow dataRow2 in table2.Rows)
                SupportTicketHints.Add((string) dataRow2[0], (string) dataRow2[1]);

            foreach (DataRow dataRow3 in table3.Rows)
                ModerationTemplates.Add(uint.Parse(dataRow3["id"].ToString()),
                    new ModerationTemplate(uint.Parse(dataRow3["id"].ToString()),
                        short.Parse(dataRow3["category"].ToString()), dataRow3["cName"].ToString(),
                        dataRow3["caption"].ToString(), dataRow3["warning_message"].ToString(),
                        dataRow3["ban_message"].ToString(), short.Parse(dataRow3["ban_hours"].ToString()),
                        dataRow3["avatar_ban"].ToString() == "1", dataRow3["mute"].ToString() == "1",
                        dataRow3["trade_lock"].ToString() == "1"));
        }

        /// <summary>
        ///     Loads the pending tickets.
        /// </summary>
        /// <param name="dbClient">The database client.</param>
        internal void LoadPendingTickets(IQueryAdapter dbClient)
        {
            /*dbClient.SetQuery("SELECT * FROM moderation_tickets");
            DataTable table = dbClient.GetTable();
            if (table == null) return;
            foreach (DataRow dataRow in table.Rows)
            {
                var ticket = new SupportTicket((uint)dataRow[0], (int)dataRow[1], (int)dataRow[2], 3, (uint)dataRow[4], (uint)dataRow[5], (string)dataRow[7], (uint)dataRow[8], (string)dataRow[9], (double)dataRow[10], new List<string>());
                this.Tickets.Add(ticket);
                //this.SupportTicketHints.Add((string)dataRow2[0], (string)dataRow2[1]);
            }*/
        }

        /// <summary>
        ///     Sends the new ticket.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="category">The category.</param>
        /// <param name="type">The type.</param>
        /// <param name="reportedUser">The reported user.</param>
        /// <param name="message">The message.</param>
        /// <param name="messages">The messages.</param>
        internal void SendNewTicket(GameClient session, int category, int type, uint reportedUser, string message,
            List<string> messages)
        {
            uint id;

            if (session.GetHabbo().CurrentRoomId <= 0)
            {
                using (IQueryAdapter dbClient = Yupi.GetDatabaseManager().GetQueryReactor())
                {
                    dbClient.SetQuery(
                        string.Concat(
                            "INSERT INTO moderation_tickets (score,type,status,sender_id,reported_id,moderator_id,message,room_id,room_name,timestamp) VALUES (1,'",
                            category, "','open','", session.GetHabbo().Id, "','", reportedUser,
                            "','0',@message,'0','','", Yupi.GetUnixTimeStamp(), "')"));
                    dbClient.AddParameter("message", message);
                    id = (uint) dbClient.InsertQuery();
                    dbClient.RunFastQuery(
                        $"UPDATE users_info SET cfhs = cfhs + 1 WHERE user_id = {session.GetHabbo().Id}");
                }

                SupportTicket ticket = new SupportTicket(id, 1, category, type, session.GetHabbo().Id, reportedUser, message, 0u,
                    "", Yupi.GetUnixTimeStamp(), messages);

                Tickets.Add(ticket);
                SendTicketToModerators(ticket);
            }
            else
            {
                RoomData data = Yupi.GetGame().GetRoomManager().GenerateNullableRoomData(session.GetHabbo().CurrentRoomId);

                using (IQueryAdapter dbClient = Yupi.GetDatabaseManager().GetQueryReactor())
                {
                    dbClient.SetQuery(
                        string.Concat(
                            "INSERT INTO moderation_tickets (score,type,status,sender_id,reported_id,moderator_id,message,room_id,room_name,timestamp) VALUES (1,'",
                            category, "','open','", session.GetHabbo().Id, "','", reportedUser, "','0',@message,'",
                            data.Id, "',@name,'", Yupi.GetUnixTimeStamp(), "')"));
                    dbClient.AddParameter("message", message);
                    dbClient.AddParameter("name", data.Name);
                    id = (uint) dbClient.InsertQuery();
                    dbClient.RunFastQuery(
                        $"UPDATE users_info SET cfhs = cfhs + 1 WHERE user_id = {session.GetHabbo().Id}");
                }

                SupportTicket ticket2 = new SupportTicket(id, 1, category, type, session.GetHabbo().Id, reportedUser, message,
                    data.Id, data.Name, Yupi.GetUnixTimeStamp(), messages);

                Tickets.Add(ticket2);
                SendTicketToModerators(ticket2);
            }
        }

        /// <summary>
        ///     Serializes the open tickets.
        /// </summary>
        /// <param name="serverMessages">The server messages.</param>
        /// <param name="userId">The user identifier.</param>
        internal void SerializeOpenTickets(ref QueuedServerMessage serverMessages, uint userId)
        {
            ServerMessage message = new ServerMessage(LibraryParser.OutgoingRequest("ModerationToolIssueMessageComposer"));

            foreach (
                SupportTicket current in
                    Tickets.Where(
                        current =>
                            current.Status == TicketStatus.Open ||
                            (current.Status == TicketStatus.Picked && current.ModeratorId == userId) ||
                            (current.Status == TicketStatus.Picked && current.ModeratorId == 0u)))
            {
                message = current.Serialize(message);
                serverMessages.AppendResponse(message);
            }
        }

        /// <summary>
        ///     Gets the ticket.
        /// </summary>
        /// <param name="ticketId">The ticket identifier.</param>
        /// <returns>SupportTicket.</returns>
        internal SupportTicket GetTicket(uint ticketId)
        {
            return Tickets.FirstOrDefault(current => current.TicketId == ticketId);
        }

        /// <summary>
        ///     Picks the ticket.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="ticketId">The ticket identifier.</param>
        internal void PickTicket(GameClient session, uint ticketId)
        {
            SupportTicket ticket = GetTicket(ticketId);

            if (ticket == null || ticket.Status != TicketStatus.Open)
                return;

            ticket.Pick(session.GetHabbo().Id, true);
            SendTicketToModerators(ticket);
        }

        /// <summary>
        ///     Releases the ticket.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="ticketId">The ticket identifier.</param>
        internal void ReleaseTicket(GameClient session, uint ticketId)
        {
            SupportTicket ticket = GetTicket(ticketId);

            if (ticket == null || ticket.Status != TicketStatus.Picked || ticket.ModeratorId != session.GetHabbo().Id)
                return;

            ticket.Release(true);
            SendTicketToModerators(ticket);
        }

        /// <summary>
        ///     Closes the ticket.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="ticketId">The ticket identifier.</param>
        /// <param name="result">The result.</param>
        internal void CloseTicket(GameClient session, uint ticketId, int result)
        {
            SupportTicket ticket = GetTicket(ticketId);

            if (ticket == null || ticket.Status != TicketStatus.Picked || ticket.ModeratorId != session.GetHabbo().Id)
                return;

            Habbo senderUser = Yupi.GetHabboById(ticket.SenderId);

            if (senderUser == null)
                return;

            uint statusCode;

            TicketStatus newStatus;

            switch (result)
            {
                case 1:
                    statusCode = 1;
                    newStatus = TicketStatus.Invalid;
                    break;

                case 2:
                    statusCode = 2;
                    newStatus = TicketStatus.Abusive;
                    break;

                default:
                    statusCode = 0;
                    newStatus = TicketStatus.Resolved;
                    break;
            }

            if (statusCode == 2)
            {
                using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                {
                    AbusiveCooldown.Add(ticket.SenderId, Yupi.GetUnixTimeStamp() + 600);
                    commitableQueryReactor.RunFastQuery(
                        $"UPDATE users_info SET cfhs_abusive = cfhs_abusive + 1 WHERE user_id = {ticket.SenderId}");
                }
            }

            GameClient senderClient = Yupi.GetGame().GetClientManager().GetClientByUserId(senderUser.Id);

            if (senderClient != null)
            {
                foreach (
                    SupportTicket current2 in
                        Tickets.FindAll(
                            current => current.ReportedId == ticket.ReportedId && current.Status == TicketStatus.Picked)
                    )
                {
                    current2.Delete(true);
                    SendTicketToModerators(current2);
                    current2.Close(newStatus, true);
                }

                senderClient.GetMessageHandler()
                    .GetResponse()
                    .Init(LibraryParser.OutgoingRequest("ModerationToolUpdateIssueMessageComposer"));
                senderClient.GetMessageHandler().GetResponse().AppendInteger(1);
                senderClient.GetMessageHandler().GetResponse().AppendInteger(ticket.TicketId);
                senderClient.GetMessageHandler().GetResponse().AppendInteger(ticket.ModeratorId);
                senderClient.GetMessageHandler()
                    .GetResponse()
                    .AppendString(Yupi.GetHabboById(ticket.ModeratorId) != null
                        ? Yupi.GetHabboById(ticket.ModeratorId).UserName
                        : "Undefined");
                senderClient.GetMessageHandler().GetResponse().AppendBool(false);
                senderClient.GetMessageHandler().GetResponse().AppendInteger(0);
                senderClient.GetMessageHandler()
                    .GetResponse()
                    .Init(LibraryParser.OutgoingRequest("ModerationTicketResponseMessageComposer"));
                senderClient.GetMessageHandler().GetResponse().AppendInteger(statusCode);
                senderClient.GetMessageHandler().SendResponse();
            }
            else
            {
                foreach (
                    SupportTicket current2 in
                        Tickets.FindAll(
                            current => current.ReportedId == ticket.ReportedId && current.Status == TicketStatus.Picked)
                    )
                {
                    current2.Delete(true);
                    SendTicketToModerators(current2);
                    current2.Close(newStatus, true);
                }
            }

            using (IQueryAdapter queryreactor2 = Yupi.GetDatabaseManager().GetQueryReactor())
                queryreactor2.RunFastQuery(
                    $"UPDATE users_stats SET tickets_answered = tickets_answered+1 WHERE id={session.GetHabbo().Id} LIMIT 1");
        }

        /// <summary>
        ///     Check if the user has an pending issue
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        internal bool UsersHasPendingTicket(uint id)
        {
            return Tickets.Any(current => current.SenderId == id && current.Status == TicketStatus.Open);
        }

        /// <summary>
        ///     Check if the previous issue of an user was abusive
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        internal bool UsersHasAbusiveCooldown(uint id)
        {
            foreach (KeyValuePair<uint, double> item in AbusiveCooldown)
            {
                if (AbusiveCooldown.ContainsKey(id) && item.Value - Yupi.GetUnixTimeStamp() > 0)
                    return true;

                AbusiveCooldown.Remove(id);
                return false;
            }
            return false;
        }

        /// <summary>
        ///     Deletes the pending ticket for user.
        /// </summary>
        /// <param name="id">The identifier.</param>
        internal void DeletePendingTicketForUser(uint id)
        {
            foreach (SupportTicket current in Tickets.Where(current => current.SenderId == id))
            {
                current.Delete(true);
                SendTicketToModerators(current);
                break;
            }
        }

        /// <summary>
        ///     Gets the pending ticket for user.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>SupportTicket.</returns>
        internal SupportTicket GetPendingTicketForUser(uint id)
        {
            return Tickets.FirstOrDefault(current => current.SenderId == id && current.Status == TicketStatus.Open);
        }

        /// <summary>
        ///     Logs the staff entry.
        /// </summary>
        /// <param name="modName">Name of the mod.</param>
        /// <param name="target">The target.</param>
        /// <param name="type">The type.</param>
        /// <param name="description">The description.</param>
        internal void LogStaffEntry(string modName, string target, string type, string description)
        {
            using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                commitableQueryReactor.SetQuery(
                    "INSERT INTO server_stafflogs (staffuser,target,action_type,description) VALUES (@Username,@target,@type,@desc)");
                commitableQueryReactor.AddParameter("Username", modName);
                commitableQueryReactor.AddParameter("target", target);
                commitableQueryReactor.AddParameter("type", type);
                commitableQueryReactor.AddParameter("desc", description);
                commitableQueryReactor.RunQuery();
            }
        }
    }
}