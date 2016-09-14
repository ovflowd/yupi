namespace Yupi.Messages.Rooms
{
    using System;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public class InitialRoomInfoMessageComposer : Yupi.Messages.Contracts.InitialRoomInfoMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, RoomData room)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendString(room.Model.DisplayName);
                message.AppendInteger(room.Id);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}