using System;
using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Events
{
    public class RoomEventMessageComposer : Contracts.RoomEventMessageComposer
    {
        public override void Compose(ISender session, RoomData room, RoomEvent roomEvent)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(room.Id);
                message.AppendInteger(room.Owner.Id);
                message.AppendString(room.Owner.Name);
                message.AppendInteger(1);
                message.AppendInteger(1);
                message.AppendString(roomEvent.Name);
                message.AppendString(roomEvent.Description);
                message.AppendInteger(0);
                throw new NotImplementedException();
                //message.AppendInteger ((int)Math.Floor ((roomEvent.Time - Yupi.GetUnixTimeStamp ()) / 60.0));

                message.AppendInteger(roomEvent.Category);

                session.Send(message);
            }
        }
    }
}