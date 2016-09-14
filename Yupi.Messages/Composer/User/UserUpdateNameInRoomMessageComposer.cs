namespace Yupi.Messages.User
{
    using System;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public class UserUpdateNameInRoomMessageComposer : Yupi.Messages.Contracts.UserUpdateNameInRoomMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender room, Habbo habbo)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(habbo.Info.Id);
                message.AppendInteger(habbo.Room.Data.Id);
                message.AppendString(habbo.Info.Name);
                room.Send(message);
            }
        }

        #endregion Methods
    }
}