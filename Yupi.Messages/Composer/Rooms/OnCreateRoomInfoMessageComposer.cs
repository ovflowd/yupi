namespace Yupi.Messages.Rooms
{
    using System;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public class OnCreateRoomInfoMessageComposer : Yupi.Messages.Contracts.OnCreateRoomInfoMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, RoomData data)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(data.Id);
                message.AppendString(data.Name);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}