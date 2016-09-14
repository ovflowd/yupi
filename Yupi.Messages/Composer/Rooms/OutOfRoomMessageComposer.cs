using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Rooms
{
    public class OutOfRoomMessageComposer : Yupi.Messages.Contracts.OutOfRoomMessageComposer
    {
        public override void Compose(Yupi.Protocol.ISender session, short code = 0)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendShort(code); // TODO Also possible without code & what does code mean.
                session.Send(message);
            }
        }
    }
}