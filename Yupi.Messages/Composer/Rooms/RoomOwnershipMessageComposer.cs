namespace Yupi.Messages.Rooms
{
    using System;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public class RoomOwnershipMessageComposer : Yupi.Messages.Contracts.RoomOwnershipMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, RoomData room, UserInfo user)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(room.Id);
                message.AppendBool(room.HasOwnerRights(user));
                session.Send(message);
            }
        }

        #endregion Methods
    }
}