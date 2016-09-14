using System;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Rooms
{
    public class DanceStatusMessageComposer : Yupi.Messages.Contracts.DanceStatusMessageComposer
    {
        // TODO Create enum for Dances
        // TODO Replace entityId with RoomEntity EVERYWHERE!
        public override void Compose(Yupi.Protocol.ISender room, int entityId, Dance dance)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(entityId);
                message.AppendInteger(dance.Value);
                room.Send(message);
            }
        }
    }
}