using Yupi.Protocol;

namespace Yupi.Messages.Rooms
{
    public class OutOfRoomMessageComposer : Contracts.OutOfRoomMessageComposer
    {
        public override void Compose(ISender session, short code = 0)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendShort(code); // TODO Also possible without code & what does code mean.
                session.Send(message);
            }
        }
    }
}