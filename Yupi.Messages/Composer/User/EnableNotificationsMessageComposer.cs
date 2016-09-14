using Yupi.Protocol;

namespace Yupi.Messages.User
{
    public class EnableNotificationsMessageComposer : Contracts.EnableNotificationsMessageComposer
    {
        public override void Compose(ISender session)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendBool(true); //isOpen
                message.AppendBool(false);
                session.Send(message);
            }
        }
    }
}