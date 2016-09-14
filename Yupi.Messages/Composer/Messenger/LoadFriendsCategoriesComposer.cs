using Yupi.Protocol;

namespace Yupi.Messages.Messenger
{
    public class LoadFriendsCategoriesComposer : Contracts.LoadFriendsCategoriesComposer
    {
        public override void Compose(ISender session)
        {
// TODO Hardcoded message
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(2000);
                message.AppendInteger(300);
                message.AppendInteger(800);
                message.AppendInteger(1100);
                message.AppendInteger(0);
                session.Send(message);
            }
        }
    }
}