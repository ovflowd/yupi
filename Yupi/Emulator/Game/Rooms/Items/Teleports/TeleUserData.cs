using Yupi.Emulator.Game.Users;
using Yupi.Emulator.Game.GameClients.Interfaces;

namespace Yupi.Emulator.Game.Rooms.Items.Teleports
{
    /// <summary>
    ///     Class TeleUserData.
    /// </summary>
     public class TeleUserData
    {
		private readonly GameClient _client;

        /// <summary>
        ///     The _room identifier
        /// </summary>
        private readonly uint _roomId;

        /// <summary>
        ///     The _tele identifier
        /// </summary>
        private readonly uint _teleId;

        /// <summary>
        ///     The _m user refference
        /// </summary>
        private readonly Habbo _userRefference;

        /// <summary>
        ///     Initializes a new instance of the <see cref="TeleUserData" /> class.
        /// </summary>
        /// <param name="handler">The p handler.</param>
        /// <param name="userRefference">The p user refference.</param>
        /// <param name="roomId">The room identifier.</param>
        /// <param name="teleId">The tele identifier.</param>
		public TeleUserData(GameClient session, Habbo userRefference, uint roomId, uint teleId)
        {
            _client = session;
            _userRefference = userRefference;
            _roomId = roomId;
            _teleId = teleId;
        }

        /// <summary>
        ///     Executes this instance.
        /// </summary>
     public void Execute()
        {
            if (_client == null || _userRefference == null) return;
            _userRefference.IsTeleporting = true;
            _userRefference.TeleporterId = _teleId;
            _client.PrepareRoomForUser(_roomId, string.Empty);
        }
    }
}