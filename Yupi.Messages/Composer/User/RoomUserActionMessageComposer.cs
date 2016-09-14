namespace Yupi.Messages.User
{
    using System;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public class RoomUserActionMessageComposer : Yupi.Messages.Contracts.RoomUserActionMessageComposer
    {
        #region Methods

        // TODO unknown param?!
        public override void Compose(Yupi.Protocol.ISender room, int virtualId, UserAction action)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(virtualId);
                message.AppendInteger(action.Value);
                room.Send(message);
            }
        }

        #endregion Methods
    }
}