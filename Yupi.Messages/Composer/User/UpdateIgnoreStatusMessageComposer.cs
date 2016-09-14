using Yupi.Protocol;

namespace Yupi.Messages.User
{
    public class UpdateIgnoreStatusMessageComposer : Contracts.UpdateIgnoreStatusMessageComposer
    {
        public override void Compose(ISender session, State state, string username)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger((int) state);
                message.AppendString(username);
                session.Send(message);
            }
        }
    }
}