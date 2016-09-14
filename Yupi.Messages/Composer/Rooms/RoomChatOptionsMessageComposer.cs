namespace Yupi.Messages.Rooms
{
    using System;

    using Yupi.Messages.Encoders;
    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public class RoomChatOptionsMessageComposer : Yupi.Messages.Contracts.RoomChatOptionsMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, RoomData data)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.Append(data.Chat);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}