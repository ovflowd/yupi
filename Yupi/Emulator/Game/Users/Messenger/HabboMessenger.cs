using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Yupi.Emulator.Core.Security.BlackWords;
using Yupi.Emulator.Core.Security.BlackWords.Enums;
using Yupi.Emulator.Core.Security.BlackWords.Structs;
using Yupi.Emulator.Data.Base.Adapters.Interfaces;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Rooms.Data;
using Yupi.Emulator.Game.Rooms.Data.Models;
using Yupi.Emulator.Game.Users.Messenger.Structs;
using Yupi.Emulator.Messages;
using Yupi.Emulator.Messages.Buffers;

namespace Yupi.Emulator.Game.Users.Messenger
{
    /// <summary>
    ///     Class HabboMessenger.
    /// </summary>
     class HabboMessenger
    {
        /// <summary>
        ///     The _user identifier
        /// </summary>
        private readonly uint _userId;

        /// <summary>
        ///     The appear offline
        /// </summary>
         bool AppearOffline;

        /// <summary>
        ///     The friends
        /// </summary>
         Dictionary<uint, MessengerBuddy> Friends;

        /// <summary>
        ///     The requests
        /// </summary>
         Dictionary<uint, MessengerRequest> Requests;

        /// <summary>
        ///     Initializes a new instance of the <see cref="HabboMessenger" /> class.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
         HabboMessenger(uint userId)
        {
            Requests = new Dictionary<uint, MessengerRequest>();
            Friends = new Dictionary<uint, MessengerBuddy>();
            _userId = userId;
        }

        /// <summary>
        ///     Initializes the specified friends.
        /// </summary>
        /// <param name="friends">The friends.</param>
        /// <param name="requests">The requests.</param>
         void Init(Dictionary<uint, MessengerBuddy> friends, Dictionary<uint, MessengerRequest> requests)
        {
            Requests = new Dictionary<uint, MessengerRequest>(requests);
            Friends = new Dictionary<uint, MessengerBuddy>(friends);
        }

        /// <summary>
        ///     Clears the requests.
        /// </summary>
         void ClearRequests()
        {
            Requests.Clear();
        }

        /// <summary>
        ///     Gets the request.
        /// </summary>
        /// <param name="senderId">The sender identifier.</param>
        /// <returns>MessengerRequest.</returns>
         MessengerRequest GetRequest(uint senderId)
            => Requests.ContainsKey(senderId) ? Requests[senderId] : null;

        /// <summary>
        ///     Destroys this instance.
        /// </summary>
         void Destroy()
        {
            IEnumerable<GameClient> clientsById = Yupi.GetGame().GetClientManager().GetClientsByUserIds(Friends.Keys);

            foreach (
                GameClient current in
                    clientsById.Where(current => current.GetHabbo() != null && current.GetHabbo().GetMessenger() != null)
                )
                current.GetHabbo().GetMessenger().UpdateFriend(_userId, null, true);

            Friends.Clear();
            Requests.Clear();

            Friends = null;
            Requests = null;
        }

        /// <summary>
        ///     Called when [status changed].
        /// </summary>
        /// <param name="notification">if set to <c>true</c> [notification].</param>
         void OnStatusChanged(bool notification)
        {
            if (Friends == null)
                return;

            IEnumerable<GameClient> clientsById = Yupi.GetGame().GetClientManager().GetClientsByUserIds(Friends.Keys);

            if (clientsById == null)
                return;

            foreach (
                GameClient current in
                    clientsById.Where(
                        current => current?.GetHabbo() != null && current.GetHabbo().GetMessenger() != null))
            {
                Habbo user = current.GetHabbo();

                HabboMessenger messenger = user?.GetMessenger();

                if (messenger != null)
                {
                    messenger.UpdateFriend(_userId, current, true);

                    UpdateFriend(user.Id, current, notification);
                }
            }
        }

        /// <summary>
        ///     Updates the friend.
        /// </summary>
        /// <param name="userid">The userid.</param>
        /// <param name="client">The client.</param>
        /// <param name="notification">if set to <c>true</c> [notification].</param>
         void UpdateFriend(uint userid, GameClient client, bool notification)
        {
            if (!Friends.ContainsKey(userid))
                return;

            Friends[userid].UpdateUser();

            if (!notification)
                return;

            GameClient client2 = GetClient();

            client2?.SendMessage(SerializeUpdate(Friends[userid]));
        }

        /// <summary>
        ///     Serializes the messenger action.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="name">The name.</param>
         void SerializeMessengerAction(int type, string name)
        {
            if (GetClient() == null)
                return;

            SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer();

            simpleServerMessageBuffer.Init(PacketLibraryManager.OutgoingHandler("ConsoleMessengerActionMessageComposer"));

            simpleServerMessageBuffer.AppendString(GetClient().GetHabbo().Id.ToString());
            simpleServerMessageBuffer.AppendInteger(type);
            simpleServerMessageBuffer.AppendString(name);

            foreach (MessengerBuddy current in Friends.Values.Where(current => current.Client != null))
                current.Client.SendMessage(simpleServerMessageBuffer);
        }

        /// <summary>
        ///     Handles all requests.
        /// </summary>
         void HandleAllRequests()
        {
            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                queryReactor.RunFastQuery("DELETE FROM messenger_requests WHERE from_id = " + _userId +
                                          " OR to_id = " + _userId);

            ClearRequests();
        }

        /// <summary>
        ///     Handles the request.
        /// </summary>
        /// <param name="sender">The sender.</param>
         void HandleRequest(uint sender)
        {
            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                queryReactor.RunFastQuery(string.Concat("DELETE FROM messenger_requests WHERE (from_id = ",
                    _userId, " AND to_id = ", sender, ") OR (to_id = ", _userId, " AND from_id = ", sender, ")"));

            Requests.Remove(sender);
        }

        /// <summary>
        ///     Creates the friendship.
        /// </summary>
        /// <param name="friendId">The friend identifier.</param>
         void CreateFriendship(uint friendId)
        {
            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                queryReactor.RunFastQuery(string.Concat("REPLACE INTO messenger_friendships (user_one_id,user_two_id) VALUES (", _userId, ",", friendId, ")"));

            OnNewFriendship(friendId);

            GameClient clientByUserId = Yupi.GetGame().GetClientManager().GetClientByUserId(friendId);

            Yupi.GetGame().GetAchievementManager().ProgressUserAchievement(clientByUserId, "ACH_FriendListSize", 1, true);

            if (clientByUserId?.GetHabbo().GetMessenger() != null)
                clientByUserId.GetHabbo().GetMessenger().OnNewFriendship(_userId);
        }

        /// <summary>
        ///     Destroys the friendship.
        /// </summary>
        /// <param name="friendId">The friend identifier.</param>
         void DestroyFriendship(uint friendId)
        {
            Habbo habbo = GetClient().GetHabbo();

            Habbo habboFriend = Yupi.GetHabboById(friendId);

            if (habbo != null && habboFriend != null)
            {
                using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                {
                    queryReactor.RunFastQuery(string.Concat("DELETE FROM messenger_friendships WHERE (user_one_id = ", habbo.Id, " AND user_two_id = ", habboFriend.Id, ")"));

                    queryReactor.RunFastQuery(string.Concat("SELECT id FROM users_relationships WHERE user_id = ", habbo.Id, " AND target = ", habboFriend.Id, " LIMIT 1"));

                    object integerResultUser = queryReactor.GetInteger();

                    int idUser;

                    int.TryParse(integerResultUser.ToString(), out idUser);

                    if (idUser > 0 && habbo.Relationships.ContainsKey(idUser))
                    {
                        queryReactor.RunFastQuery(string.Concat("DELETE FROM users_relationships WHERE (user_id = ", habbo.Id, " AND target = ", habboFriend.Id, ")"));

                        if (habbo.Relationships.ContainsKey(idUser))
                            habbo.Relationships.Remove(idUser);
                    }

                    queryReactor.RunFastQuery(string.Concat("DELETE FROM messenger_friendships WHERE (user_one_id = ", habboFriend.Id, " AND user_two_id = ", habbo.Id, ")"));

                    queryReactor.RunFastQuery(string.Concat("SELECT id FROM users_relationships WHERE user_id = ", habboFriend.Id, " AND target = ", habbo.Id, " LIMIT 1"));

                    object integerResultFriend = queryReactor.GetInteger();

                    int idFriend;

                    int.TryParse(integerResultFriend.ToString(), out idFriend);

                    if (idFriend > 0 && habboFriend.Relationships.ContainsKey(idFriend))
                    {
                        queryReactor.RunFastQuery(string.Concat("DELETE FROM users_relationships WHERE (user_id = ", habboFriend.Id, " AND target = ", habbo.Id, ")"));

                        if (habboFriend.Relationships.ContainsKey(idFriend))
                            habboFriend.Relationships.Remove(idFriend);
                    }
                }

                OnDestroyFriendship(habboFriend.Id);

                habboFriend.GetMessenger().OnDestroyFriendship(habbo.Id);
            }
        }

        /// <summary>
        ///     Called when [new friendship].
        /// </summary>
        /// <param name="friendId">The friend identifier.</param>
         void OnNewFriendship(uint friendId)
        {
            GameClient clientByUserId = Yupi.GetGame().GetClientManager().GetClientByUserId(friendId);

            MessengerBuddy messengerBuddy;

            if (clientByUserId?.GetHabbo() == null)
            {
                using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                {
                    queryReactor.SetQuery(
                        $"SELECT id,username,motto,look,last_online,hide_inroom,hide_online FROM users WHERE id = {friendId}");

                    DataRow row = queryReactor.GetRow();

                    messengerBuddy = new MessengerBuddy(friendId, (string) row["username"], (string) row["look"],
                        (string) row["motto"], Yupi.EnumToBool(row["hide_online"].ToString()),
                        Yupi.EnumToBool(row["hide_inroom"].ToString()));
                }
            }
            else
            {
                Habbo habbo = clientByUserId.GetHabbo();

                messengerBuddy = new MessengerBuddy(friendId, habbo.UserName, habbo.Look, habbo.Motto,
                    habbo.AppearOffline, habbo.HideInRoom);

                messengerBuddy.UpdateUser();
            }

            if (!Friends.ContainsKey(friendId))
                Friends.Add(friendId, messengerBuddy);

            GetClient().SendMessage(SerializeUpdate(messengerBuddy));
        }

        /// <summary>
        ///     Requests the exists.
        /// </summary>
        /// <param name="requestId">The request identifier.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
         bool RequestExists(uint requestId) => Requests != null && Requests.ContainsKey(requestId);

        /// <summary>
        ///     Friendships the exists.
        /// </summary>
        /// <param name="friendId">The friend identifier.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
         bool FriendshipExists(uint friendId) => Friends.ContainsKey(friendId);

        /// <summary>
        ///     Called when [destroy friendship].
        /// </summary>
        /// <param name="friend">The friend.</param>
         void OnDestroyFriendship(uint friend)
        {
            if(Friends.ContainsKey(friend))
                Friends.Remove(friend);

            GetClient().GetMessageHandler().GetResponse().Init(PacketLibraryManager.OutgoingHandler("FriendUpdateMessageComposer"));
            GetClient().GetMessageHandler().GetResponse().AppendInteger(0);
            GetClient().GetMessageHandler().GetResponse().AppendInteger(1);
            GetClient().GetMessageHandler().GetResponse().AppendInteger(-1);
            GetClient().GetMessageHandler().GetResponse().AppendInteger(friend);

            GetClient().GetMessageHandler().SendResponse();
        }

        /// <summary>
        ///     Requests the buddy.
        /// </summary>
        /// <param name="userQuery">The user query.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
         bool RequestBuddy(string userQuery)
        {
            GameClient clientByUsername = Yupi.GetGame().GetClientManager().GetClientByUserName(userQuery);

            uint userId;
            bool blockForNewFriends;

            if (clientByUsername == null)
            {
                DataRow dataRow;

                using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                {
                    queryReactor.SetQuery("SELECT id, block_newfriends FROM users WHERE username = @query");

                    queryReactor.AddParameter("query", userQuery.ToLower());
                    dataRow = queryReactor.GetRow();
                }

                if (dataRow == null)
                    return false;

                userId = Convert.ToUInt32(dataRow["id"]);

                blockForNewFriends = Yupi.EnumToBool(dataRow["block_newfriends"].ToString());
            }
            else
            {
                Habbo currentUser = clientByUsername.GetHabbo();

                userId = currentUser.Id;

                blockForNewFriends = currentUser.HasFriendRequestsDisabled;
            }

            GameClient client = GetClient();

            if (blockForNewFriends && client.GetHabbo().Rank < 4)
            {
                client.GetMessageHandler()
                    .GetResponse()
                    .Init(PacketLibraryManager.OutgoingHandler("NotAcceptingRequestsMessageComposer"));

                client.GetMessageHandler().GetResponse().AppendInteger(39);
                client.GetMessageHandler().GetResponse().AppendInteger(3);
                client.GetMessageHandler().SendResponse();

                return false;
            }

            if (RequestExists(userId))
            {
                client.SendNotif("Você já enviou um pedido de amizade anteriormente.");
                //@todo: Mudar Texto para sistema de langs

                return true;
            }

            using (IQueryAdapter queryreactor2 = Yupi.GetDatabaseManager().GetQueryReactor())
                queryreactor2.RunFastQuery(string.Concat("REPLACE INTO messenger_requests (from_id,to_id) VALUES (",
                    _userId, ",", userId, ")"));

            Habbo fromUser = client.GetHabbo();

            if (clientByUsername?.GetHabbo() != null && fromUser != null)
            {
                MessengerRequest messengerRequest = new MessengerRequest(userId, _userId, fromUser.UserName, fromUser.Look);

                clientByUsername.GetHabbo().GetMessenger().OnNewRequest(_userId, messengerRequest);

                SimpleServerMessageBuffer simpleServerMessageBuffer =
                    new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("ConsoleSendFriendRequestMessageComposer"));

                messengerRequest.Serialize(simpleServerMessageBuffer);
                clientByUsername.SendMessage(simpleServerMessageBuffer);
            }

            return true;
        }

        /// <summary>
        ///     Called when [new request].
        /// </summary>
        /// <param name="friendId">The friend identifier.</param>
        /// <param name="friendRequest"></param>
         void OnNewRequest(uint friendId, MessengerRequest friendRequest)
        {
            if (!Requests.ContainsKey(friendId))
                Requests.Add(friendId, friendRequest);
        }

        /// <summary>
        ///     Sends the instant message.
        /// </summary>
        /// <param name="toId">To identifier.</param>
        /// <param name="message">The message.</param>
         void SendInstantMessage(uint toId, string message)
        {
            if (string.IsNullOrEmpty(message))
                return;

            if (toId != 0)
            {
                BlackWord word;

                if (BlackWordsManager.Check(message, BlackWordType.Hotel, out word))
                {
                    BlackWordTypeSettings settings = word.TypeSettings;

                    GameClient thisClient = GetClient();

                    if (thisClient != null)
                    {
                        thisClient.HandlePublicist(word.Word, message, "MESSENGER", settings);

                        if (settings.ShowMessage)
                        {
                            thisClient.SendModeratorMessage("A mensagem contém a palavra: " + word.Word + " que não é permitida, você poderá ser banido!");

                            return;
                        }
                    }
                }
            }

            if (!FriendshipExists(toId))
            {
                DeliverInstantMessageError(6, toId);
                return;
            }

            if (toId == 0) // Staff Chat
            {
                SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("ConsoleChatMessageComposer"));

                simpleServerMessageBuffer.AppendInteger(0); //userid
                simpleServerMessageBuffer.AppendString(GetClient().GetHabbo().UserName + " : " + message);
                simpleServerMessageBuffer.AppendInteger(0);

                if (GetClient().GetHabbo().Rank >= Yupi.StaffAlertMinRank)
                    Yupi.GetGame().GetClientManager().StaffAlert(simpleServerMessageBuffer, GetClient().GetHabbo().Id);
                else if (GetClient().GetHabbo().Rank >= Convert.ToUInt32(Yupi.GetDbConfig().DbData["ambassador.minrank"]))
                    Yupi.GetGame().GetClientManager().AmbassadorAlert(simpleServerMessageBuffer, GetClient().GetHabbo().Id);
            }
            else
            {
                GameClient clientByUserId = Yupi.GetGame().GetClientManager().GetClientByUserId(toId);

                if (clientByUserId?.GetHabbo().GetMessenger() == null)
                {
                    if (!Yupi.OfflineMessages.ContainsKey(toId))
                        Yupi.OfflineMessages.Add(toId, new List<OfflineMessage>());

                    Yupi.OfflineMessages[toId].Add(new OfflineMessage(GetClient().GetHabbo().Id, message,
                        Yupi.GetUnixTimeStamp()));

                    OfflineMessage.SaveMessage(Yupi.GetDatabaseManager().GetQueryReactor(), toId,
                        GetClient().GetHabbo().Id, message);

                    return;
                }

                if (GetClient().GetHabbo().Muted)
                {
                    DeliverInstantMessageError(4, toId);

                    return;
                }

                if (clientByUserId.GetHabbo().Muted)
                    DeliverInstantMessageError(3, toId);

                if (message == string.Empty)
                    return;

                clientByUserId.GetHabbo().GetMessenger().DeliverInstantMessage(message, _userId);
            }
        }

        /// <summary>
        ///     Delivers the instant message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="convoId">The convo identifier.</param>
         void DeliverInstantMessage(string message, uint convoId)
        {
            SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("ConsoleChatMessageComposer"));

            simpleServerMessageBuffer.AppendInteger(convoId);
            simpleServerMessageBuffer.AppendString(message);
            simpleServerMessageBuffer.AppendInteger(0);

            GetClient().SendMessage(simpleServerMessageBuffer);
        }

        /// <summary>
        ///     Delivers the instant message error.
        /// </summary>
        /// <param name="errorId">The error identifier.</param>
        /// <param name="conversationId">The conversation identifier.</param>
         void DeliverInstantMessageError(int errorId, uint conversationId)
        {
            SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("ConsoleChatErrorMessageComposer"));

            simpleServerMessageBuffer.AppendInteger(errorId);
            simpleServerMessageBuffer.AppendInteger(conversationId);
            simpleServerMessageBuffer.AppendString(string.Empty);

            GetClient().SendMessage(simpleServerMessageBuffer);
        }

        /// <summary>
        ///     Serializes the categories.
        /// </summary>
        /// <returns>SimpleServerMessageBuffer.</returns>
         SimpleServerMessageBuffer SerializeCategories()
        {
            SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("LoadFriendsCategories"));

            simpleServerMessageBuffer.AppendInteger(2000);
            simpleServerMessageBuffer.AppendInteger(300);
            simpleServerMessageBuffer.AppendInteger(800);
            simpleServerMessageBuffer.AppendInteger(1100);
            simpleServerMessageBuffer.AppendInteger(0);

            return simpleServerMessageBuffer;
        }

        /// <summary>
        ///     Serializes the friends.
        /// </summary>
        /// <returns>SimpleServerMessageBuffer.</returns>
         SimpleServerMessageBuffer SerializeFriends()
        {
            SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("LoadFriendsMessageComposer"));

            simpleServerMessageBuffer.AppendInteger(1);
            simpleServerMessageBuffer.AppendInteger(0);
            simpleServerMessageBuffer.AppendInteger(Friends.Count);

            GameClient client = GetClient();

            foreach (MessengerBuddy current in Friends.Values)
            {
                current.UpdateUser();
                current.Serialize(simpleServerMessageBuffer, client);
            }

            return simpleServerMessageBuffer;
        }

        /// <summary>
        ///     Serializes the offline messages.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>SimpleServerMessageBuffer.</returns>
         SimpleServerMessageBuffer SerializeOfflineMessages(OfflineMessage message)
        {
            SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("ConsoleChatMessageComposer"));

            simpleServerMessageBuffer.AppendInteger(message.FromId);
            simpleServerMessageBuffer.AppendString(message.Message);
            simpleServerMessageBuffer.AppendInteger((int) (Yupi.GetUnixTimeStamp() - message.Timestamp));

            return simpleServerMessageBuffer;
        }

        /// <summary>
        ///     Serializes the update.
        /// </summary>
        /// <param name="friend">The friend.</param>
        /// <returns>SimpleServerMessageBuffer.</returns>
         SimpleServerMessageBuffer SerializeUpdate(MessengerBuddy friend)
        {
            SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("FriendUpdateMessageComposer"));

            simpleServerMessageBuffer.AppendInteger(0);
            simpleServerMessageBuffer.AppendInteger(1);
            simpleServerMessageBuffer.AppendInteger(0);
            friend.Serialize(simpleServerMessageBuffer, GetClient());
            simpleServerMessageBuffer.AppendBool(false);

            return simpleServerMessageBuffer;
        }

        /// <summary>
        ///     Serializes the requests.
        /// </summary>
        /// <returns>SimpleServerMessageBuffer.</returns>
         SimpleServerMessageBuffer SerializeRequests()
        {
            SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("FriendRequestsMessageComposer"));
            simpleServerMessageBuffer.AppendInteger(Requests.Count > Yupi.FriendRequestLimit
                ? (int) Yupi.FriendRequestLimit
                : Requests.Count);
            simpleServerMessageBuffer.AppendInteger(Requests.Count > Yupi.FriendRequestLimit
                ? (int) Yupi.FriendRequestLimit
                : Requests.Count);

            IEnumerable<MessengerRequest> requests = Requests.Values.Take((int) Yupi.FriendRequestLimit);

            foreach (MessengerRequest current in requests)
                current.Serialize(simpleServerMessageBuffer);

            return simpleServerMessageBuffer;
        }

        /// <summary>
        ///     Performs the search.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>SimpleServerMessageBuffer.</returns>
         SimpleServerMessageBuffer PerformSearch(string query)
        {
            List<SearchResult> searchResult = SearchResultFactory.GetSearchResult(query);

            List<SearchResult> list = new List<SearchResult>();
            List<SearchResult> list2 = new List<SearchResult>();

            foreach (SearchResult current in searchResult)
            {
                if (FriendshipExists(current.UserId))
                    list.Add(current);
                else
                    list2.Add(current);
            }

            SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("ConsoleSearchFriendMessageComposer"));

            simpleServerMessageBuffer.AppendInteger(list.Count);

            foreach (SearchResult current2 in list)
                current2.Searialize(simpleServerMessageBuffer);

            simpleServerMessageBuffer.AppendInteger(list2.Count);

            foreach (SearchResult current3 in list2)
                current3.Searialize(simpleServerMessageBuffer);

            return simpleServerMessageBuffer;
        }

        /// <summary>
        ///     Gets the active friends rooms.
        /// </summary>
        /// <returns>HashSet&lt;GetPublicRoomData&gt;.</returns>
         HashSet<RoomData> GetActiveFriendsRooms()
        {
            HashSet<RoomData> toReturn = new HashSet<RoomData>();

            foreach (MessengerBuddy current in Friends.Values.Where(p => p != null && p.InRoom && p.CurrentRoom?.RoomData != null))
                toReturn.Add(current.CurrentRoom.RoomData);

            return toReturn;
        }

        /// <summary>
        ///     Gets the client.
        /// </summary>
        /// <returns>GameClient.</returns>
        private GameClient GetClient() => Yupi.GetGame().GetClientManager().GetClientByUserId(_userId);
    }
}