using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.User
{
    public class UserBadgesMessageComposer : Contracts.UserBadgesMessageComposer
    {
        public override void Compose(ISender session, UserInfo user)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(user.Id);
                var badges = user.Badges.GetVisible();

                message.AppendInteger(badges.Count);

                foreach (var badge in badges)
                {
                    message.AppendInteger(badge.Slot);
                    message.AppendString(badge.Code);
                }

                session.Send(message);
            }
        }
    }
}