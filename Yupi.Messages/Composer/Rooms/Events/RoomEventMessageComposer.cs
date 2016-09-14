namespace Yupi.Messages.Events
{
    using System;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public class RoomEventMessageComposer : Yupi.Messages.Contracts.RoomEventMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, RoomData room, RoomEvent roomEvent)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
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

        #endregion Methods
    }
}