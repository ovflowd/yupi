namespace Yupi.Messages.Rooms
{
    using System;
    using System.Data;
    using System.Linq;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public class LoadRoomRightsListMessageComposer : Yupi.Messages.Contracts.LoadRoomRightsListMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, RoomData room)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(room.Id);
                message.AppendInteger(room.Rights.Count);

                foreach (UserInfo habboForId in room.Rights)
                {
                    message.AppendInteger(habboForId.Id);
                    message.AppendString(habboForId.Name);
                }

                session.Send(message);
            }
        }

        #endregion Methods
    }
}