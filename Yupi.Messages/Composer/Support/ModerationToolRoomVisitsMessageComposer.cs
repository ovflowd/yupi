namespace Yupi.Messages.Support
{
    using System;
    using System.Linq;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public class ModerationToolRoomVisitsMessageComposer : Yupi.Messages.Contracts.ModerationToolRoomVisitsMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, UserInfo user)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(user.Id);
                message.AppendString(user.Name);
                message.AppendInteger(user.RecentlyVisitedRooms.Count);
                // TODO Refactor
                foreach (RoomData roomData in user.RecentlyVisitedRooms)
                {
                    message.AppendInteger(roomData.Id);
                    message.AppendString(roomData.Name);
                    // TODO Implement
                    message.AppendInteger(0); //hour
                    message.AppendInteger(0); //min
                }

                session.Send(message);
            }
        }

        #endregion Methods
    }
}