using Yupi.Model.Domain.Components;
using Yupi.Protocol;

namespace Yupi.Messages.User
{
    public class LoadBadgesWidgetMessageComposer : Contracts.LoadBadgesWidgetMessageComposer
    {
        public override void Compose(ISender session, UserBadgeComponent badges)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(badges.Badges.Count);

                foreach (var badge in badges.Badges)
                {
                    message.AppendInteger(1); // TODO Magic constant
                    message.AppendString(badge.Code);
                }

                var visibleBadges = badges.GetVisible();

                message.AppendInteger(visibleBadges.Count);

                foreach (var badge in visibleBadges)
                {
                    message.AppendInteger(badge.Slot);
                    message.AppendString(badge.Code);
                }

                session.Send(message);
            }
        }
    }
}