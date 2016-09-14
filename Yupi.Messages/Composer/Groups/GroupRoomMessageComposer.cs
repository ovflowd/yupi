namespace Yupi.Messages.Groups
{
    using System;

    using Yupi.Protocol.Buffers;

    public class GroupRoomMessageComposer : Yupi.Messages.Contracts.GroupRoomMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, int roomId, int groupId)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(roomId);
                message.AppendInteger(groupId);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}