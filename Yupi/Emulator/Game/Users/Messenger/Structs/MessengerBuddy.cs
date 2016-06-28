using System;
using System.Linq;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Rooms;
using Yupi.Emulator.Game.Users.Relationships;


namespace Yupi.Emulator.Game.Users.Messenger.Structs
{
    /// <summary>
    ///     Class MessengerBuddy.
    /// </summary>
     public class MessengerBuddy
    {
        //private readonly int _lastOnline;
        /// <summary>
        ///     The _appear offline
        /// </summary>
        private readonly bool _appearOffline;

        /// <summary>
        ///     The _hide inroom
        /// </summary>
        private readonly bool _hideInroom;

        /// <summary>
        ///     The _look
        /// </summary>
        private string _look;

        /// <summary>
        ///     The _motto
        /// </summary>
        private string _motto;

        /// <summary>
        ///     The client
        /// </summary>
     public GameClient Client;

        /// <summary>
        ///     The user name
        /// </summary>
     public string UserName;

        /// <summary>
        ///     Initializes a new instance of the <see cref="MessengerBuddy" /> class.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="look">The look.</param>
        /// <param name="motto">The motto.</param>
        /// <param name="appearOffline">if set to <c>true</c> [appear offline].</param>
        /// <param name="hideInroom">if set to <c>true</c> [hide inroom].</param>
     public MessengerBuddy(uint userId, string userName, string look, string motto, bool appearOffline,
            bool hideInroom)
        {
            Id = userId;
            UserName = userName;
            _look = look;
            _motto = motto;
            _appearOffline = appearOffline;
            _hideInroom = hideInroom;
        }

        /// <summary>
        ///     Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
     public uint Id { get; set; }

        /// <summary>
        ///     Gets a value indicating whether this instance is online.
        /// </summary>
        /// <value><c>true</c> if this instance is online; otherwise, <c>false</c>.</value>
     public bool IsOnline
            =>
                Client?.GetHabbo() != null && Client.GetHabbo().GetMessenger() != null &&
                !Client.GetHabbo().GetMessenger().AppearOffline;

        /// <summary>
        ///     Gets a value indicating whether [in room].
        /// </summary>
        /// <value><c>true</c> if [in room]; otherwise, <c>false</c>.</value>
     public bool InRoom => CurrentRoom != null;

        /// <summary>
        ///     Gets or sets the current room.
        /// </summary>
        /// <value>The current room.</value>
     public Room CurrentRoom { get; set; }

        /// <summary>
        ///     Updates the user.
        /// </summary>
     public void UpdateUser()
        {
            Client = Yupi.GetGame().GetClientManager().GetClientByUserId(Id);

            if (Client?.GetHabbo() == null)
                return;

            CurrentRoom = Client.GetHabbo().CurrentRoom;
            _look = Client.GetHabbo().Look;
            _motto = Client.GetHabbo().Motto;
        }

        /// <summary>
        ///     Serializes the specified messageBuffer.
        /// </summary>
        /// <param name="message">The messageBuffer.</param>
        /// <param name="session">The session.</param>
     public void Serialize(SimpleServerMessageBuffer messageBuffer, GameClient session)
        {
            Relationship value =
                session.GetHabbo().Relationships.FirstOrDefault(x => x.Value.UserId == Convert.ToInt32(Id)).Value;

            int i = value?.Type ?? 0;

            messageBuffer.AppendInteger(Id);
            messageBuffer.AppendString(UserName);
            messageBuffer.AppendInteger(IsOnline || Id == 0);

            messageBuffer.AppendBool(Id == 0 || (!_appearOffline || session.GetHabbo().Rank >= 4) && IsOnline);
            messageBuffer.AppendBool(Id != 0 && (!_hideInroom || session.GetHabbo().Rank >= 4) && InRoom);

            messageBuffer.AppendString(IsOnline || Id == 0 ? _look : string.Empty);

            messageBuffer.AppendInteger(0);
            messageBuffer.AppendString(_motto);
            messageBuffer.AppendString(string.Empty);
            messageBuffer.AppendString(string.Empty);
            messageBuffer.AppendBool(true);
            messageBuffer.AppendBool(false);
            messageBuffer.AppendBool(false);
            messageBuffer.AppendShort(i);
        }
    }
}