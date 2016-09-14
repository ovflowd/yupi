namespace Yupi.Messages.Rooms
{
    using System;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public class CanCreateRoomMessageComposer : Yupi.Messages.Contracts.CanCreateRoomMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, UserInfo user)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(user.UsersRooms.Count >= 75 ? 1 : 0); // TODO Enum
                message.AppendInteger(75); // TODO Magic number
                session.Send(message);
            }
        }

        #endregion Methods
    }
}