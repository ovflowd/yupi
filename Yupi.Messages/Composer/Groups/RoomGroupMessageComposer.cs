namespace Yupi.Messages.Groups
{
    using System;
    using System.Collections.Generic;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public class RoomGroupMessageComposer : Yupi.Messages.Contracts.RoomGroupMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender room, ISet<Group> groups)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(groups.Count);

                foreach (Group current in groups)
                {
                    message.AppendInteger(current.Id);
                    message.AppendString(current.Badge);
                }

                room.Send(message);
            }
        }

        #endregion Methods
    }
}