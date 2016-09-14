using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Rooms
{
    public class RoomEnterErrorMessageComposer : Yupi.Messages.Contracts.RoomEnterErrorMessageComposer
    {
        public override void Compose(Yupi.Protocol.ISender session, Error error)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger((int) error);
                session.Send(message);
            }
        }
    }
}