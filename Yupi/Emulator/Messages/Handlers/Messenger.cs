using System;
using System.Collections.Generic;
using System.Linq;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Users.Messenger.Structs;
using Yupi.Emulator.Messages.Buffers;

namespace Yupi.Emulator.Messages.Handlers
{
    /// <summary>
    ///     Class MessageHandler.
    /// </summary>
     partial class MessageHandler
    {
        /// <summary>
        ///     Friendses the list update.
        /// </summary>
         void FriendsListUpdate()
        {
            Session.GetHabbo().GetMessenger();
        }

        /// <summary>
        ///     Removes the buddy.
        /// </summary>
         void RemoveBuddy()
        {
            if (Session.GetHabbo().GetMessenger() == null) return;
            int num = Request.GetInteger();
            for (int i = 0; i < num; i++)
            {
                uint num2 = Request.GetUInt32();
                if (Session.GetHabbo().Relationships.ContainsKey(Convert.ToInt32(num2)))
                {
                    Session.SendNotif(Yupi.GetLanguage().GetVar("buddy_error_1"));
                    return;
                }
                Session.GetHabbo().GetMessenger().DestroyFriendship(num2);
            }
        }

        /// <summary>
        ///     Searches the habbo.
        /// </summary>
         void SearchHabbo()
        {
            if (Session.GetHabbo().GetMessenger() == null) return;
            Session.SendMessage(Session.GetHabbo().GetMessenger().PerformSearch(Request.GetString()));
        }

        /// <summary>
        ///     Accepts the request.
        /// </summary>
         void AcceptRequest()
        {
            if (Session.GetHabbo().GetMessenger() == null) return;
            int num = Request.GetInteger();
            for (int i = 0; i < num; i++)
            {
                uint num2 = Request.GetUInt32();
                MessengerRequest request = Session.GetHabbo().GetMessenger().GetRequest(num2);
                if (request == null) continue;
                if (request.To != Session.GetHabbo().Id) return;
                if (!Session.GetHabbo().GetMessenger().FriendshipExists(request.To))
                    Session.GetHabbo().GetMessenger().CreateFriendship(request.From);
                Session.GetHabbo().GetMessenger().HandleRequest(num2);
            }
        }

        /// <summary>
        ///     Declines the request.
        /// </summary>
         void DeclineRequest()
        {
            if (Session.GetHabbo().GetMessenger() == null) return;
            bool flag = Request.GetBool();
            Request.GetInteger();
            if (!flag)
            {
                uint sender = Request.GetUInt32();
                Session.GetHabbo().GetMessenger().HandleRequest(sender);
                return;
            }
            Session.GetHabbo().GetMessenger().HandleAllRequests();
        }

        /// <summary>
        ///     Requests the buddy.
        /// </summary>
         void RequestBuddy()
        {
            if (Session.GetHabbo().GetMessenger() == null)
                return;

            Session.GetHabbo().GetMessenger().RequestBuddy(Request.GetString());
        }

        /// <summary>
        ///     Sends the instant messenger.
        /// </summary>
         void SendInstantMessenger()
        {
            uint toId = Request.GetUInt32();
            string text = Request.GetString();
            if (Session.GetHabbo().GetMessenger() == null) return;
            if (!string.IsNullOrWhiteSpace(text)) Session.GetHabbo().GetMessenger().SendInstantMessage(toId, text);
        }

        /// <summary>
        ///     Follows the buddy.
        /// </summary>
         void FollowBuddy()
        {
            uint userId = Request.GetUInt32();
            GameClient clientByUserId = Yupi.GetGame().GetClientManager().GetClientByUserId(userId);

            if (clientByUserId == null || clientByUserId.GetHabbo() == null) return;
            if (clientByUserId.GetHabbo().GetMessenger() == null || clientByUserId.GetHabbo().CurrentRoom == null)
            {
                if (Session.GetHabbo().GetMessenger() == null) return;
                Response.Init(PacketLibraryManager.OutgoingHandler("FollowFriendErrorMessageComposer"));
                Response.AppendInteger(2);
                SendResponse();
                Session.GetHabbo().GetMessenger().UpdateFriend(userId, clientByUserId, true);
                return;
            }
            if (Session.GetHabbo().Rank < 4 && Session.GetHabbo().GetMessenger() != null &&
                !Session.GetHabbo().GetMessenger().FriendshipExists(userId))
            {
                Response.Init(PacketLibraryManager.OutgoingHandler("FollowFriendErrorMessageComposer"));
                Response.AppendInteger(0);
                SendResponse();
                return;
            }

            SimpleServerMessageBuffer roomFwd = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("RoomForwardMessageComposer"));
            roomFwd.AppendInteger(clientByUserId.GetHabbo().CurrentRoom.RoomId);
            Session.SendMessage(roomFwd);
        }

        /// <summary>
        ///     Sends the instant invite.
        /// </summary>
         void SendInstantInvite()
        {
            int num = Request.GetInteger();
            List<uint> list = new List<uint>();
            for (int i = 0; i < num; i++) list.Add(Request.GetUInt32());
            string s = Request.GetString();
            SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("ConsoleInvitationMessageComposer"));
            simpleServerMessageBuffer.AppendInteger(Session.GetHabbo().Id);
            simpleServerMessageBuffer.AppendString(s);
            foreach (GameClient clientByUserId in (from current in list
                where Session.GetHabbo().GetMessenger().FriendshipExists(current)
                select Yupi.GetGame().GetClientManager().GetClientByUserId(current))
                .TakeWhile(
                    clientByUserId => clientByUserId != null))
                clientByUserId.SendMessage(simpleServerMessageBuffer);
        }
    }
}