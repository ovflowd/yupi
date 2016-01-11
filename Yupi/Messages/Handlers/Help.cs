using System.Collections.Generic;
using System.Globalization;
using Yupi.Game.GameClients.Interfaces;
using Yupi.Game.Rooms;
using Yupi.Game.Rooms.Data;
using Yupi.Game.Support;
using Yupi.Messages.Parsers;

namespace Yupi.Messages.Handlers
{
    /// <summary>
    ///     Class GameClientMessageHandler.
    /// </summary>
    internal partial class GameClientMessageHandler
    {
        /// <summary>
        ///     Initializes the help tool.
        /// </summary>
        internal void InitHelpTool()
        {
            Response.Init(LibraryParser.OutgoingRequest("OpenHelpToolMessageComposer"));

            if (!Yupi.GetGame().GetModerationTool().UsersHasPendingTicket(Session.GetHabbo().Id))
            {
                Response.AppendInteger(0); // It's okay, the user may send an new issue
                SendResponse();
                return;
            }

            SupportTicket ticket = Yupi.GetGame().GetModerationTool().GetPendingTicketForUser(Session.GetHabbo().Id);

            if (ticket == null) // null check to be sure
                return;

            Response.AppendInteger(1); // the user has an pending issue

            Response.AppendString(ticket.TicketId.ToString());
            Response.AppendString(ticket.Timestamp.ToString(CultureInfo.InvariantCulture));
            Response.AppendString(ticket.Message);
            SendResponse();
        }

        /// <summary>
        ///     Submits the help ticket.
        /// </summary>
        internal void SubmitHelpTicket()
        {
            string message = Request.GetString();
            int category = Request.GetInteger();
            uint reportedUser = Request.GetUInteger();

            Request.GetUInteger(); // roomId

            int messageCount = Request.GetInteger();

            List<string> chats = new List<string>();

            for (int i = 0; i < messageCount; i++)
            {
                Request.GetInteger();

                chats.Add(Request.GetString());
            }

            Response.Init(LibraryParser.OutgoingRequest("TicketUserAlert"));

            if (Yupi.GetGame().GetModerationTool().UsersHasPendingTicket(Session.GetHabbo().Id))
            {
                SupportTicket ticket = Yupi.GetGame().GetModerationTool().GetPendingTicketForUser(Session.GetHabbo().Id);
                Response.AppendInteger(1);
                Response.AppendString(ticket.TicketId.ToString());
                Response.AppendString(ticket.Timestamp.ToString(CultureInfo.InvariantCulture));
                Response.AppendString(ticket.Message);
                SendResponse();

                return;
            }

            if (Yupi.GetGame().GetModerationTool().UsersHasAbusiveCooldown(Session.GetHabbo().Id))
                // the previous issue of the user was abusive
            {
                Response.AppendInteger(2);
                SendResponse();

                return;
            }

            Response.AppendInteger(0); // It's okay, the user may send an new issue
            Yupi.GetGame().GetModerationTool().SendNewTicket(Session, category, 7, reportedUser, message, chats);

            SendResponse();
        }

        /// <summary>
        ///     Deletes the pending CFH.
        /// </summary>
        internal void DeletePendingCfh()
        {
            if (!Yupi.GetGame().GetModerationTool().UsersHasPendingTicket(Session.GetHabbo().Id))
                return;

            Yupi.GetGame().GetModerationTool().DeletePendingTicketForUser(Session.GetHabbo().Id);

            Response.Init(LibraryParser.OutgoingRequest("OpenHelpToolMessageComposer"));
            Response.AppendInteger(0);
            SendResponse();
        }

        /// <summary>
        ///     Mods the get user information.
        /// </summary>
        internal void ModGetUserInfo()
        {
            if (Session.GetHabbo().HasFuse("fuse_mod"))
            {
                uint num = Request.GetUInteger();

                if (Yupi.GetGame().GetClientManager().GetNameById(num) != "Unknown User")
                    Session.SendMessage(ModerationTool.SerializeUserInfo(num));
                else
                    Session.SendNotif(Yupi.GetLanguage().GetVar("help_information_error"));
            }
        }

        /// <summary>
        ///     Mods the get user chatlog.
        /// </summary>
        internal void ModGetUserChatlog()
        {
            if (!Session.GetHabbo().HasFuse("fuse_chatlogs"))
                return;

            Session.SendMessage(ModerationTool.SerializeUserChatlog(Request.GetUInteger()));
        }

        /// <summary>
        ///     Mods the get room chatlog.
        /// </summary>
        internal void ModGetRoomChatlog()
        {
            if (!Session.GetHabbo().HasFuse("fuse_chatlogs"))
            {
                Session.SendNotif(Yupi.GetLanguage().GetVar("help_information_error_rank_low"));
                return;
            }

            Request.GetInteger();
            uint roomId = Request.GetUInteger();

            if (Yupi.GetGame().GetRoomManager().GetRoom(roomId) != null)
                Session.SendMessage(ModerationTool.SerializeRoomChatlog(roomId));
        }

        /// <summary>
        ///     Mods the get room tool.
        /// </summary>
        internal void ModGetRoomTool()
        {
            if (!Session.GetHabbo().HasFuse("fuse_mod"))
                return;

            uint roomId = Request.GetUInteger();
            RoomData data = Yupi.GetGame().GetRoomManager().GenerateNullableRoomData(roomId);

            Session.SendMessage(ModerationTool.SerializeRoomTool(data));
        }

        /// <summary>
        ///     Mods the pick ticket.
        /// </summary>
        internal void ModPickTicket()
        {
            if (!Session.GetHabbo().HasFuse("fuse_mod"))
                return;

            Request.GetInteger();
            uint ticketId = Request.GetUInteger();

            Yupi.GetGame().GetModerationTool().PickTicket(Session, ticketId);
        }

        /// <summary>
        ///     Mods the release ticket.
        /// </summary>
        internal void ModReleaseTicket()
        {
            if (!Session.GetHabbo().HasFuse("fuse_mod"))
                return;

            int ticketCount = Request.GetInteger();

            for (int i = 0; i < ticketCount; i++)
                Yupi.GetGame().GetModerationTool().ReleaseTicket(Session, Request.GetUInteger());
        }

        /// <summary>
        ///     Mods the close ticket.
        /// </summary>
        internal void ModCloseTicket()
        {
            if (!Session.GetHabbo().HasFuse("fuse_mod"))
                return;

            int result = Request.GetInteger();

            Request.GetInteger();

            uint ticketId = Request.GetUInteger();

            if (ticketId <= 0)
                return;

            Yupi.GetGame().GetModerationTool().CloseTicket(Session, ticketId, result);
        }

        /// <summary>
        ///     Mods the get ticket chatlog.
        /// </summary>
        internal void ModGetTicketChatlog()
        {
            if (!Session.GetHabbo().HasFuse("fuse_mod"))
                return;

            SupportTicket ticket = Yupi.GetGame().GetModerationTool().GetTicket(Request.GetUInteger());

            if (ticket == null)
                return;

            RoomData roomData = Yupi.GetGame().GetRoomManager().GenerateNullableRoomData(ticket.RoomId);

            if (roomData == null)
                return;

            Session.SendMessage(ModerationTool.SerializeTicketChatlog(ticket, roomData, ticket.Timestamp));
        }

        /// <summary>
        ///     Mods the get room visits.
        /// </summary>
        internal void ModGetRoomVisits()
        {
            if (Session.GetHabbo().HasFuse("fuse_mod"))
            {
                uint userId = Request.GetUInteger();

                if (userId > 0)
                    Session.SendMessage(ModerationTool.SerializeRoomVisits(userId));
            }
        }

        /// <summary>
        ///     Mods the send room alert.
        /// </summary>
        internal void ModSendRoomAlert()
        {
            if (!Session.GetHabbo().HasFuse("fuse_alert"))
                return;

            Request.GetInteger();

            string message = Request.GetString();

            ServerMessage serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("SuperNotificationMessageComposer"));
            serverMessage.AppendString("admin");
            serverMessage.AppendInteger(3);
            serverMessage.AppendString("message");
            serverMessage.AppendString($"{message}\r\n\r\n- {Session.GetHabbo().UserName}");
            serverMessage.AppendString("link");
            serverMessage.AppendString("event:");
            serverMessage.AppendString("linkTitle");
            serverMessage.AppendString("ok");

            Room room = Session.GetHabbo().CurrentRoom;

            room?.SendMessage(serverMessage);
        }

        /// <summary>
        ///     Mods the perform room action.
        /// </summary>
        internal void ModPerformRoomAction()
        {
            if (!Session.GetHabbo().HasFuse("fuse_mod"))
                return;

            uint roomId = Request.GetUInteger();
            bool lockRoom = Request.GetIntegerAsBool();
            bool inappropriateRoom = Request.GetIntegerAsBool();
            bool kickUsers = Request.GetIntegerAsBool();

            ModerationTool.PerformRoomAction(Session, roomId, kickUsers, lockRoom, inappropriateRoom, Response);
        }

        /// <summary>
        ///     Mods the send user caution.
        /// </summary>
        internal void ModSendUserCaution()
        {
            if (!Session.GetHabbo().HasFuse("fuse_alert"))
                return;

            uint userId = Request.GetUInteger();
            string message = Request.GetString();

            ModerationTool.AlertUser(Session, userId, message, true);
        }

        /// <summary>
        ///     Mods the send user message.
        /// </summary>
        internal void ModSendUserMessage()
        {
            if (!Session.GetHabbo().HasFuse("fuse_alert"))
                return;

            uint userId = Request.GetUInteger();
            string message = Request.GetString();

            ModerationTool.AlertUser(Session, userId, message, false);
        }

        /// <summary>
        ///     Mods the mute user.
        /// </summary>
        internal void ModMuteUser()
        {
            if (!Session.GetHabbo().HasFuse("fuse_mute"))
                return;

            uint userId = Request.GetUInteger();
            string message = Request.GetString();
            GameClient clientByUserId = Yupi.GetGame().GetClientManager().GetClientByUserId(userId);

            clientByUserId.GetHabbo().Mute();
            clientByUserId.SendNotif(message);
        }

        /// <summary>
        ///     Mods the lock trade.
        /// </summary>
        internal void ModLockTrade()
        {
            if (!Session.GetHabbo().HasFuse("fuse_lock_trade"))
                return;

            uint userId = Request.GetUInteger();
            string message = Request.GetString();
            int length = Request.GetInteger()*3600;

            ModerationTool.LockTrade(Session, userId, message, length);
        }

        /// <summary>
        ///     Mods the kick user.
        /// </summary>
        internal void ModKickUser()
        {
            if (!Session.GetHabbo().HasFuse("fuse_kick"))
                return;

            uint userId = Request.GetUInteger();
            string message = Request.GetString();

            ModerationTool.KickUser(Session, userId, message, false);
        }

        /// <summary>
        ///     Mods the ban user.
        /// </summary>
        internal void ModBanUser()
        {
            if (!Session.GetHabbo().HasFuse("fuse_ban"))
                return;

            uint userId = Request.GetUInteger();
            string message = Request.GetString();
            int length = Request.GetInteger()*3600;

            ModerationTool.BanUser(Session, userId, length, message);
        }
    }
}