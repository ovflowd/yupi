using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.User
{
    public class UpdateAvatarAspectMessageComposer : Contracts.UpdateAvatarAspectMessageComposer
    {
        public override void Compose(ISender session, UserInfo habbo)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendString(habbo.Look);
                message.AppendString(habbo.Gender.ToUpper()); // TODO Make sure gender is stored UPPER
                session.Send(message);
            }
        }
    }
}