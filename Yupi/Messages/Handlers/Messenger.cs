using System;
using System.Collections.Generic;
using System.Linq;
using Yupi.Game.GameClients.Interfaces;
using Yupi.Game.Users.Messenger.Structs;
using Yupi.Messages.Parsers;

namespace Yupi.Messages.Handlers
{
    /// <summary>
    ///     Class GameClientMessageHandler.
    /// </summary>
    internal partial class GameClientMessageHandler
    {
        /// <summary>
        ///     Friendses the list update.
        /// </summary>
        internal void FriendsListUpdate()
        {
            Session.GetHabbo().GetMessenger();
        }

        /// <summary>
        ///     Removes the buddy.
        /// </summary>
        internal void RemoveBuddy()
        {
            if (Session.GetHabbo().GetMessenger() == null) return;
            int num = Request.GetInteger();
            for (int i = 0; i < num; i++)
            {
                uint num2 = Request.GetUInteger();
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
        internal void SearchHabbo()
        {
            if (Session.GetHabbo().GetMessenger() == null) return;
            Session.SendMessage(Session.GetHabbo().GetMessenger().PerformSearch(Request.GetString()));
        }

        /// <summary>
        ///     Accepts the request.
        /// </summary>
        internal void AcceptRequest()
        {
            if (Session.GetHabbo().GetMessenger() == null) return;
            int num = Request.GetInteger();
            for (int i = 0; i < num; i++)
            {
                uint num2 = Request.GetUInteger();
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
        internal void DeclineRequest()
        {
            if (Session.GetHabbo().GetMessenger() == null) return;
            bool flag = Request.GetBool();
            Request.GetInteger();
            if (!flag)
            {
                uint sender = Request.GetUInteger();
                Session.GetHabbo().GetMessenger().HandleRequest(sender);
                return;
            }
            Session.GetHabbo().GetMessenger().HandleAllRequests();
        }

        /// <summary>
        ///     Requests the buddy.
        /// </summary>
        internal void RequestBuddy()
        {
            if (Session.GetHabbo().GetMessenger() == null)
                return;

            Session.GetHabbo().GetMessenger().RequestBuddy(Request.GetString());
        }

        /// <summary>
        ///     Sends the instant messenger.
        /// </summary>
        internal void SendInstantMessenger()
        {
            uint toId = Request.GetUInteger();
            string text = Request.GetString();
            if (Session.GetHabbo().GetMessenger() == null) return;
            if (!string.IsNullOrWhiteSpace(text)) Session.GetHabbo().GetMessenger().SendInstantMessage(toId, text);
        }

        /// <summary>
        ///     Follows the buddy.
        /// </summary>
        internal void FollowBuddy()
        {
            uint userId = Request.GetUInteger();
            GameClient clientByUserId = Yupi.GetGame().GetClientManager().GetClientByUserId(userId);

            if (clientByUserId == null || clientByUserId.GetHabbo() == null) return;
            if (clientByUserId.GetHabbo().GetMessenger() == null || clientByUserId.GetHabbo().CurrentRoom == null)
            {
                if (Session.GetHabbo().GetMessenger() == null) return;
                Response.Init(LibraryParser.OutgoingRequest("FollowFriendErrorMessageComposer"));
                Response.AppendInteger(2);
                SendResponse();
                Session.GetHabbo().GetMessenger().UpdateFriend(userId, clientByUserId, true);
                return;
            }
            if (Session.GetHabbo().Rank < 4 && Session.GetHabbo().GetMessenger() != null &&
                !Session.GetHabbo().GetMessenger().FriendshipExists(userId))
            {
                Response.Init(LibraryParser.OutgoingRequest("FollowFriendErrorMessageComposer"));
                Response.AppendInteger(0);
                SendResponse();
                return;
            }

            ServerMessage roomFwd = new ServerMessage(LibraryParser.OutgoingRequest("RoomForwardMessageComposer"));
            roomFwd.AppendInteger(clientByUserId.GetHabbo().CurrentRoom.RoomId);
            Session.SendMessage(roomFwd);
        }

        /// <summary>
        ///     Sends the instant invite.
        /// </summary>
        internal void SendInstantInvite()
        {
            int num = Request.GetInteger();
            List<uint> list = new List<uint>();
            for (int i = 0; i < num; i++) list.Add(Request.GetUInteger());
            string s = Request.GetString();
            ServerMessage serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("ConsoleInvitationMessageComposer"));
            serverMessage.AppendInteger(Session.GetHabbo().Id);
            serverMessage.AppendString(s);
            foreach (GameClient clientByUserId in (from current in list
                where Session.GetHabbo().GetMessenger().FriendshipExists(current)
                select Yupi.GetGame().GetClientManager().GetClientByUserId(current))
                .TakeWhile(
                    clientByUserId => clientByUserId != null))
                clientByUserId.SendMessage(serverMessage);
        }
    }
}