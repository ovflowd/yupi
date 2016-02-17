using Yupi.Game.Users;
using Yupi.Messages.Handlers;

namespace Yupi.Game.Rooms.Items.Teleports
{
    /// <summary>
    ///     Class TeleUserData.
    /// </summary>
    internal class TeleUserData
    {
        /// <summary>
        ///     The _m handler
        /// </summary>
        private readonly GameClientMessageHandler _mHandler;

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
        internal TeleUserData(GameClientMessageHandler handler, Habbo userRefference, uint roomId, uint teleId)
        {
            _mHandler = handler;
            _userRefference = userRefference;
            _roomId = roomId;
            _teleId = teleId;
        }

        /// <summary>
        ///     Executes this instance.
        /// </summary>
        internal void Execute()
        {
            if (_mHandler == null || _userRefference == null) return;
            _userRefference.IsTeleporting = true;
            _userRefference.TeleporterId = _teleId;
            _mHandler.PrepareRoomForUser(_roomId, string.Empty);
        }
    }
}