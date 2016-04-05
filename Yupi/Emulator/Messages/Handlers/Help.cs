using System.Collections.Generic;
using System.Globalization;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Rooms;
using Yupi.Emulator.Game.Rooms.Data;
using Yupi.Emulator.Game.Rooms.Data.Models;
using Yupi.Emulator.Game.Support;
using Yupi.Emulator.Messages.Buffers;

namespace Yupi.Emulator.Messages.Handlers
{
    /// <summary>
    ///     Class MessageHandler.
    /// </summary>
     partial class MessageHandler
    {
        /// <summary>
        ///     Initializes the help tool.
        /// </summary>
         void InitHelpTool()
        {
            Response.Init(PacketLibraryManager.OutgoingHandler("OpenHelpToolMessageComposer"));

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
         void SubmitHelpTicket()
        {
            string message = Request.GetString();
            int category = Request.GetInteger();
            uint reportedUser = Request.GetUInt32();

            Request.GetUInt32(); // roomId

            int messageCount = Request.GetInteger();

            List<string> chats = new List<string>();

            for (int i = 0; i < messageCount; i++)
            {
                Request.GetInteger();

                chats.Add(Request.GetString());
            }

            Response.Init(PacketLibraryManager.OutgoingHandler("TicketUserAlert"));

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
         void DeletePendingCfh()
        {
            if (!Yupi.GetGame().GetModerationTool().UsersHasPendingTicket(Session.GetHabbo().Id))
                return;

            Yupi.GetGame().GetModerationTool().DeletePendingTicketForUser(Session.GetHabbo().Id);

            Response.Init(PacketLibraryManager.OutgoingHandler("OpenHelpToolMessageComposer"));
            Response.AppendInteger(0);
            SendResponse();
        }

        /// <summary>
        ///     Mods the get user information.
        /// </summary>
         void ModGetUserInfo()
        {
            if (Session.GetHabbo().HasFuse("fuse_mod"))
            {
                uint num = Request.GetUInt32();

                if (Yupi.GetGame().GetClientManager().GetUserNameByUserId(num) != "Unknown User")
                    Session.SendMessage(ModerationTool.SerializeUserInfo(num));
                else
                    Session.SendNotif(Yupi.GetLanguage().GetVar("help_information_error"));
            }
        }

        /// <summary>
        ///     Mods the get user chatlog.
        /// </summary>
         void ModGetUserChatlog()
        {
            if (!Session.GetHabbo().HasFuse("fuse_chatlogs"))
                return;

            Session.SendMessage(ModerationTool.SerializeUserChatlog(Request.GetUInt32()));
        }

        /// <summary>
        ///     Mods the get room chatlog.
        /// </summary>
         void ModGetRoomChatlog()
        {
            if (!Session.GetHabbo().HasFuse("fuse_chatlogs"))
            {
                Session.SendNotif(Yupi.GetLanguage().GetVar("help_information_error_rank_low"));
                return;
            }

            Request.GetInteger();
            uint roomId = Request.GetUInt32();

            if (Yupi.GetGame().GetRoomManager().GetRoom(roomId) != null)
                Session.SendMessage(ModerationTool.SerializeRoomChatlog(roomId));
        }

        /// <summary>
        ///     Mods the get room tool.
        /// </summary>
         void ModGetRoomTool()
        {
            if (!Session.GetHabbo().HasFuse("fuse_mod"))
                return;

            uint roomId = Request.GetUInt32();
            RoomData data = Yupi.GetGame().GetRoomManager().GenerateNullableRoomData(roomId);

            Session.SendMessage(ModerationTool.SerializeRoomTool(data));
        }

        /// <summary>
        ///     Mods the pick ticket.
        /// </summary>
         void ModPickTicket()
        {
            if (!Session.GetHabbo().HasFuse("fuse_mod"))
                return;

            Request.GetInteger();
            uint ticketId = Request.GetUInt32();

            Yupi.GetGame().GetModerationTool().PickTicket(Session, ticketId);
        }

        /// <summary>
        ///     Mods the release ticket.
        /// </summary>
         void ModReleaseTicket()
        {
            if (!Session.GetHabbo().HasFuse("fuse_mod"))
                return;

            int ticketCount = Request.GetInteger();

            for (int i = 0; i < ticketCount; i++)
                Yupi.GetGame().GetModerationTool().ReleaseTicket(Session, Request.GetUInt32());
        }

        /// <summary>
        ///     Mods the close ticket.
        /// </summary>
         void ModCloseTicket()
        {
            if (!Session.GetHabbo().HasFuse("fuse_mod"))
                return;

            int result = Request.GetInteger();

            Request.GetInteger();

            uint ticketId = Request.GetUInt32();

            if (ticketId <= 0)
                return;

            Yupi.GetGame().GetModerationTool().CloseTicket(Session, ticketId, result);
        }

        /// <summary>
        ///     Mods the get ticket chatlog.
        /// </summary>
         void ModGetTicketChatlog()
        {
            if (!Session.GetHabbo().HasFuse("fuse_mod"))
                return;

            SupportTicket ticket = Yupi.GetGame().GetModerationTool().GetTicket(Request.GetUInt32());

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
         void ModGetRoomVisits()
        {
            if (Session.GetHabbo().HasFuse("fuse_mod"))
            {
                uint userId = Request.GetUInt32();

                if (userId > 0)
                    Session.SendMessage(ModerationTool.SerializeRoomVisits(userId));
            }
        }

        /// <summary>
        ///     Mods the send room alert.
        /// </summary>
         void ModSendRoomAlert()
        {
            if (!Session.GetHabbo().HasFuse("fuse_alert"))
                return;

            Request.GetInteger();

            string message = Request.GetString();

            SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("SuperNotificationMessageComposer"));
            simpleServerMessageBuffer.AppendString("admin");
            simpleServerMessageBuffer.AppendInteger(3);
            simpleServerMessageBuffer.AppendString("message");
            simpleServerMessageBuffer.AppendString($"{message}\r\n\r\n- {Session.GetHabbo().UserName}");
            simpleServerMessageBuffer.AppendString("link");
            simpleServerMessageBuffer.AppendString("event:");
            simpleServerMessageBuffer.AppendString("linkTitle");
            simpleServerMessageBuffer.AppendString("ok");

            Room room = Session.GetHabbo().CurrentRoom;

            room?.SendMessage(simpleServerMessageBuffer);
        }

        /// <summary>
        ///     Mods the perform room action.
        /// </summary>
         void ModPerformRoomAction()
        {
            if (!Session.GetHabbo().HasFuse("fuse_mod"))
                return;

            uint roomId = Request.GetUInt32();
            bool lockRoom = Request.GetIntegerAsBool();
            bool inappropriateRoom = Request.GetIntegerAsBool();
            bool kickUsers = Request.GetIntegerAsBool();

            ModerationTool.PerformRoomAction(Session, roomId, kickUsers, lockRoom, inappropriateRoom, Response);
        }

        /// <summary>
        ///     Mods the send user caution.
        /// </summary>
         void ModSendUserCaution()
        {
            if (!Session.GetHabbo().HasFuse("fuse_alert"))
                return;

            uint userId = Request.GetUInt32();
            string message = Request.GetString();

            ModerationTool.AlertUser(Session, userId, message, true);
        }

        /// <summary>
        ///     Mods the send user message.
        /// </summary>
         void ModSendUserMessage()
        {
            if (!Session.GetHabbo().HasFuse("fuse_alert"))
                return;

            uint userId = Request.GetUInt32();
            string message = Request.GetString();

            ModerationTool.AlertUser(Session, userId, message, false);
        }

        /// <summary>
        ///     Mods the mute user.
        /// </summary>
         void ModMuteUser()
        {
            if (!Session.GetHabbo().HasFuse("fuse_mute"))
                return;

            uint userId = Request.GetUInt32();
            string message = Request.GetString();
            GameClient clientByUserId = Yupi.GetGame().GetClientManager().GetClientByUserId(userId);

            clientByUserId.GetHabbo().Mute();
            clientByUserId.SendNotif(message);
        }

        /// <summary>
        ///     Mods the lock trade.
        /// </summary>
         void ModLockTrade()
        {
            if (!Session.GetHabbo().HasFuse("fuse_lock_trade"))
                return;

            uint userId = Request.GetUInt32();
            string message = Request.GetString();
            int length = Request.GetInteger()*3600;

            ModerationTool.LockTrade(Session, userId, message, length);
        }

        /// <summary>
        ///     Mods the kick user.
        /// </summary>
         void ModKickUser()
        {
            if (!Session.GetHabbo().HasFuse("fuse_kick"))
                return;

            uint userId = Request.GetUInt32();
            string message = Request.GetString();

            ModerationTool.KickUser(Session, userId, message, false);
        }

        /// <summary>
        ///     Mods the ban user.
        /// </summary>
         void ModBanUser()
        {
            if (!Session.GetHabbo().HasFuse("fuse_ban"))
                return;

            uint userId = Request.GetUInt32();
            string message = Request.GetString();
            int length = Request.GetInteger()*3600;

            ModerationTool.BanUser(Session, userId, length, message);
        }
    }
}