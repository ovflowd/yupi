namespace Yupi.Messages.Rooms
{
    using System;

    using Yupi.Protocol.Buffers;

    public class RoomSettingsSavedMessageComposer : Yupi.Messages.Contracts.RoomSettingsSavedMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, int roomId)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(roomId);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}