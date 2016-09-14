namespace Yupi.Messages.User
{
    using System;

    using Yupi.Net;
    using Yupi.Protocol;
    using Yupi.Protocol.Buffers;

    public class GiveRespectsMessageComposer : Yupi.Messages.Contracts.GiveRespectsMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender room, int user, int respect)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(user);
                message.AppendInteger(respect);
                room.Send(message);
            }
        }

        #endregion Methods
    }
}