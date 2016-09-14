using System;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Rooms
{
    public class GiveRoomRightsMessageComposer : Yupi.Messages.Contracts.GiveRoomRightsMessageComposer
    {
        public override void Compose(Yupi.Protocol.ISender session, int roomId, UserInfo habbo)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(roomId);
                message.AppendInteger(habbo.Id);
                message.AppendString(habbo.Name);
                session.Send(message);
            }
        }
    }
}