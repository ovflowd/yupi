namespace Yupi.Messages.Rooms
{
    using System;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public class GiveRoomRightsMessageComposer : Yupi.Messages.Contracts.GiveRoomRightsMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, int roomId, UserInfo habbo)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(roomId);
                message.AppendInteger(habbo.Id);
                message.AppendString(habbo.Name);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}