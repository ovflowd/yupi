using System;
using Yupi.Protocol.Buffers;
using System.Linq;
using Yupi.Model.Domain;

namespace Yupi.Messages.Support
{
    public class ModerationToolRoomVisitsMessageComposer :
        Yupi.Messages.Contracts.ModerationToolRoomVisitsMessageComposer
    {
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
    }
}