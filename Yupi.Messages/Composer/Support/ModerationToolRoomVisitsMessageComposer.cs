using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Support
{
    public class ModerationToolRoomVisitsMessageComposer : Contracts.ModerationToolRoomVisitsMessageComposer
    {
        public override void Compose(ISender session, UserInfo user)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(user.Id);
                message.AppendString(user.Name);
                message.AppendInteger(user.RecentlyVisitedRooms.Count);
                // TODO Refactor
                foreach (var roomData in user.RecentlyVisitedRooms)
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