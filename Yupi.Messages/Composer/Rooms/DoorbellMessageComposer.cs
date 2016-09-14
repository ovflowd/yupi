using Yupi.Protocol;

namespace Yupi.Messages.Rooms
{
    public class DoorbellMessageComposer : Contracts.DoorbellMessageComposer
    {
        public override void Compose(ISender session, string username)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendString(username);
                session.Send(message);
            }
        }
    }
}