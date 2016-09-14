using Yupi.Protocol;

namespace Yupi.Messages.User
{
    public class UpdateUsernameMessageComposer : Contracts.UpdateUsernameMessageComposer
    {
        public override void Compose(ISender session, string newName)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(0); // TODO Magic constant
                message.AppendString(newName);
                message.AppendInteger(0);
                session.Send(message);
            }
        }
    }
}