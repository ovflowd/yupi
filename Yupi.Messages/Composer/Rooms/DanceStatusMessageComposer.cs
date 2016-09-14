using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Rooms
{
    public class DanceStatusMessageComposer : Contracts.DanceStatusMessageComposer
    {
        // TODO Create enum for Dances
        // TODO Replace entityId with RoomEntity EVERYWHERE!
        public override void Compose(ISender room, int entityId, Dance dance)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(entityId);
                message.AppendInteger(dance.Value);
                room.Send(message);
            }
        }
    }
}