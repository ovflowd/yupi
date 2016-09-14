using Yupi.Protocol;

namespace Yupi.Messages.Rooms
{
    public class FloorMapMessageComposer : Contracts.FloorMapMessageComposer
    {
        public override void Compose(ISender session, string heightmap, int wallHeight)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendBool(true); // TODO Hardcoded
                message.AppendInteger(wallHeight);
                message.AppendString(heightmap);
                session.Send(message);
            }
        }
    }
}