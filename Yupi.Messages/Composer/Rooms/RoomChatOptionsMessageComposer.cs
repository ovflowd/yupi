using System;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;
using Yupi.Messages.Encoders;

namespace Yupi.Messages.Rooms
{
    public class RoomChatOptionsMessageComposer : Yupi.Messages.Contracts.RoomChatOptionsMessageComposer
    {
        public override void Compose(Yupi.Protocol.ISender session, RoomData data)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.Append(data.Chat);
                session.Send(message);
            }
        }
    }
}