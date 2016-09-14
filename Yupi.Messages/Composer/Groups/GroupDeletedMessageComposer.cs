namespace Yupi.Messages.Groups
{
    using System;

    using Yupi.Protocol.Buffers;

    public class GroupDeletedMessageComposer : Yupi.Messages.Contracts.GroupDeletedMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender room, int groupId)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(groupId);
                room.Send(message);
            }
        }

        #endregion Methods
    }
}