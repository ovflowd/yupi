using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Notification
{
    public class UsersClassificationMessageComposer : Contracts.UsersClassificationMessageComposer
    {
        public override void Compose(ISender session, UserInfo habbo, string word)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(1);
                message.AppendInteger(habbo.Id);
                message.AppendString(habbo.Name);
                message.AppendString("BadWord: " + word);
                session.Send(message);
            }
        }
    }
}