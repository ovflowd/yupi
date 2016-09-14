using Yupi.Protocol;

namespace Yupi.Messages.Rooms
{
    public class FloodFilterMessageComposer : Contracts.FloodFilterMessageComposer
    {
        public override void Compose(ISender session, int remainingSeconds)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(remainingSeconds);
                session.Send(message);
            }
        }
    }
}