using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using Yupi.Emulator.Data.Base.Adapters.Interfaces;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Rooms;
using Yupi.Emulator.Game.Rooms.Chat;
using Yupi.Emulator.Game.Rooms.Data;
using Yupi.Emulator.Game.Rooms.Data.Models;
using Yupi.Emulator.Game.Users;



namespace Yupi.Emulator.Game.Support
{
    /// <summary>
    ///     Class ModerationTool.
    /// </summary>
    public class ModerationTool
    {
        /// <summary>
        ///     Abusive suppot ticket cooldown
        /// </summary>
     public Dictionary<uint, double> AbusiveCooldown;

        /// <summary>
        ///     The moderation templates
        /// </summary>
     public Dictionary<uint, ModerationTemplate> ModerationTemplates;

        /// <summary>
        ///     The room messageBuffer presets
        /// </summary>
     public List<string> RoomMessagePresets;

        /// <summary>
        ///     The support ticket hints
        /// </summary>
     public StringDictionary SupportTicketHints;

        /// <summary>
        ///     The tickets
        /// </summary>
     public List<SupportTicket> Tickets;

        /// <summary>
        ///     The user messageBuffer presets
        /// </summary>
     public List<string> UserMessagePresets;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ModerationTool" /> class.
        /// </summary>
     public ModerationTool()
        {
            Tickets = new List<SupportTicket>();
            UserMessagePresets = new List<string>();
            RoomMessagePresets = new List<string>();
            SupportTicketHints = new StringDictionary();
            ModerationTemplates = new Dictionary<uint, ModerationTemplate>();
            AbusiveCooldown = new Dictionary<uint, double>();
        }

          /// <summary>
        ///     Sends the ticket to moderators.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
     public static void SendTicketToModerators(SupportTicket ticket)
        {
			Router.GetComposer<ModerationToolIssueMessageComposer> ().Compose (Yupi.GetGame ().GetClientManager ().StaffAlert, ticket);   
        }

        /// <summary>
        ///     Performs the room action.
        /// </summary>
        /// <param name="modSession">The mod session.</param>
        /// <param name="roomId">The room identifier.</param>
        /// <param name="kickUsers">if set to <c>true</c> [kick users].</param>
        /// <param name="lockRoom">if set to <c>true</c> [lock room].</param>
        /// <param name="inappropriateRoom">if set to <c>true</c> [inappropriate room].</param>
        /// <param name="messageBuffer">The messageBuffer.</param>
     public static void PerformRoomAction(GameClient modSession, uint roomId, bool kickUsers, bool lockRoom,
            bool inappropriateRoom)
        {
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(roomId);

            if (room == null)
                return;

            if (lockRoom)
            {
                room.RoomData.State = 1;

                using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                    queryReactor.RunFastQuery(
                        $"UPDATE rooms_data SET state = 'locked' WHERE id = {room.RoomId}");
            }

            if (inappropriateRoom)
            {
				// TODO Translate
                room.RoomData.Name = "Inapropriado para a Gerência do Hotel";
                room.RoomData.Description = "A descrição do quarto não é permitida.";
                room.ClearTags();
				router.GetComposer<RoomDataMessageComposer> ().Compose (room, room, true, true);
            }

            if (kickUsers)
                room.OnRoomKick();
        }

        /// <summary>
        ///     Mods the action result.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="result">if set to <c>true</c> [result].</param>
     public static void ModActionResult(uint userId, bool result)
        {
            GameClient clientByUserId = Yupi.GetGame().GetClientManager().GetClientByUserId(userId);

            clientByUserId.GetMessageHandler().GetResponse().Init(PacketLibraryManager.OutgoingHandler("ModerationActionResultMessageComposer"));
            clientByUserId.GetMessageHandler().GetResponse().AppendInteger(userId);
            clientByUserId.GetMessageHandler().GetResponse().AppendBool(false);
            clientByUserId.GetMessageHandler().SendResponse();
        }

        /// <summary>
        ///     Kicks the user.
        /// </summary>
        /// <param name="modSession">The mod session.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="message">The messageBuffer.</param>
        /// <param name="soft">if set to <c>true</c> [soft].</param>
     public static void KickUser(GameClient modSession, uint userId, string message, bool soft)
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

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                queryReactor.RunFastQuery(
                    $"UPDATE users_info SET cautions = cautions + 1 WHERE user_id = {userId}");
        }

        /// <summary>
        ///     Alerts the user.
        /// </summary>
        /// <param name="modSession">The mod session.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="message">The messageBuffer.</param>
        /// <param name="caution">if set to <c>true</c> [caution].</param>
     public static void AlertUser(GameClient modSession, uint userId, string message, bool caution)
        {
            GameClient clientByUserId = Yupi.GetGame().GetClientManager().GetClientByUserId(userId);

            clientByUserId?.SendModeratorMessage(message);
        }

        /// <summary>
        ///     Locks the trade.
        /// </summary>
        /// <param name="modSession">The mod session.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="message">The messageBuffer.</param>
        /// <param name="length">The length.</param>
     public static void LockTrade(GameClient modSession, uint userId, string message, int length)
        {
            GameClient clientByUserId = Yupi.GetGame().GetClientManager().GetClientByUserId(userId);

            if (clientByUserId == null)
                return;

            if (!clientByUserId.GetHabbo().CheckTrading())
                length += Yupi.GetUnixTimeStamp() - clientByUserId.GetHabbo().TradeLockExpire;

            clientByUserId.GetHabbo().TradeLocked = true;
            clientByUserId.GetHabbo().TradeLockExpire = Yupi.GetUnixTimeStamp() + length;
            clientByUserId.SendNotif(message);

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                queryReactor.RunFastQuery(
                    $"UPDATE users SET trade_lock = '1', trade_lock_expire = '{clientByUserId.GetHabbo().TradeLockExpire}' WHERE id = '{clientByUserId.GetHabbo().Id}'");
        }

        /// <summary>
        ///     Bans the user.
        /// </summary>
        /// <param name="modSession">The mod session.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="length">The length.</param>
        /// <param name="message">The messageBuffer.</param>
     public static void BanUser(GameClient modSession, uint userId, int length, string message)
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
        ///     Loads the messageBuffer presets.
        /// </summary>
        /// <param name="dbClient">The database client.</param>
     public void LoadMessagePresets(IQueryAdapter dbClient)
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
     public void LoadPendingTickets(IQueryAdapter dbClient)
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
        /// <param name="message">The messageBuffer.</param>
        /// <param name="messages">The messages.</param>
     public void SendNewTicket(GameClient session, int category, int type, uint reportedUser, string message, List<string> messages)
        {
            uint id;

            if (session.GetHabbo().CurrentRoomId <= 0)
            {
                using (IQueryAdapter dbClient = Yupi.GetDatabaseManager().GetQueryReactor())
                {
                    dbClient.SetQuery(string.Concat("INSERT INTO moderation_tickets (score,type,status,sender_id,reported_id,moderator_id,message,room_id,room_name,timestamp) VALUES (1,'", category, "','open','", session.GetHabbo().Id, "','", reportedUser, "','0',@message,'0','','", Yupi.GetUnixTimeStamp(), "')"));

                    dbClient.AddParameter("message", message);

                    id = (uint) dbClient.InsertQuery();

                    dbClient.RunFastQuery($"UPDATE users_info SET cfhs = cfhs + 1 WHERE user_id = {session.GetHabbo().Id}");
                }

                SupportTicket ticket = new SupportTicket(id, 1, category, type, session.GetHabbo().Id, reportedUser, message, 0u, "", Yupi.GetUnixTimeStamp(), messages);

                Tickets.Add(ticket);
                SendTicketToModerators(ticket);
            }
            else
            {
                RoomData data = Yupi.GetGame().GetRoomManager().GenerateNullableRoomData(session.GetHabbo().CurrentRoomId);

                using (IQueryAdapter dbClient = Yupi.GetDatabaseManager().GetQueryReactor())
                {
                    dbClient.SetQuery(string.Concat("INSERT INTO moderation_tickets (score,type,status,sender_id,reported_id,moderator_id,message,room_id,room_name,timestamp) VALUES (1,'", category, "','open','", session.GetHabbo().Id, "','", reportedUser, "','0',@message,'", data.Id, "',@name,'", Yupi.GetUnixTimeStamp(), "')"));

                    dbClient.AddParameter("message", message);
                    dbClient.AddParameter("name", data.Name);

                    id = (uint) dbClient.InsertQuery();

                    dbClient.RunFastQuery($"UPDATE users_info SET cfhs = cfhs + 1 WHERE user_id = {session.GetHabbo().Id}");
                }

                SupportTicket ticket2 = new SupportTicket(id, 1, category, type, session.GetHabbo().Id, reportedUser, message, data.Id, data.Name, Yupi.GetUnixTimeStamp(), messages);

                Tickets.Add(ticket2);
                SendTicketToModerators(ticket2);
            }
        }

        /// <summary>
        ///     Gets the ticket.
        /// </summary>
        /// <param name="ticketId">The ticket identifier.</param>
        /// <returns>SupportTicket.</returns>
     public SupportTicket GetTicket(uint ticketId)
        {
            return Tickets.FirstOrDefault(current => current.TicketId == ticketId);
        }

        /// <summary>
        ///     Picks the ticket.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="ticketId">The ticket identifier.</param>
     public void PickTicket(GameClient session, uint ticketId)
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
     public void ReleaseTicket(GameClient session, uint ticketId)
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
     public void CloseTicket(GameClient session, uint ticketId, int result)
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
                using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                {
                    AbusiveCooldown.Add(ticket.SenderId, Yupi.GetUnixTimeStamp() + 600);
                    queryReactor.RunFastQuery(
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
                    .Init(PacketLibraryManager.OutgoingHandler("ModerationToolUpdateIssueMessageComposer"));
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
                    .Init(PacketLibraryManager.OutgoingHandler("ModerationTicketResponseMessageComposer"));
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
     public bool UsersHasPendingTicket(uint id)
        {
            return Tickets.Any(current => current.SenderId == id && current.Status == TicketStatus.Open);
        }

        /// <summary>
        ///     Check if the previous issue of an user was abusive
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
     public bool UsersHasAbusiveCooldown(uint id)
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
     public void DeletePendingTicketForUser(uint id)
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
     public SupportTicket GetPendingTicketForUser(uint id)
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
     public void LogStaffEntry(string modName, string target, string type, string description)
        {
            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.SetQuery(
                    "INSERT INTO server_stafflogs (staffuser,target,action_type,description) VALUES (@Username,@target,@type,@desc)");
                queryReactor.AddParameter("Username", modName);
                queryReactor.AddParameter("target", target);
                queryReactor.AddParameter("type", type);
                queryReactor.AddParameter("desc", description);
                queryReactor.RunQuery();
            }
        }
    }
}