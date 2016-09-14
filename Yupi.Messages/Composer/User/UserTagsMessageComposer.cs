using Yupi.Messages.Encoders;
using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.User
{
    public class UserTagsMessageComposer : Contracts.UserTagsMessageComposer
    {
        public override void Compose(ISender session, UserInfo info)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(info.Id);
                message.Append(info.Tags);
                session.Send(message);
            }
        }
    }
}