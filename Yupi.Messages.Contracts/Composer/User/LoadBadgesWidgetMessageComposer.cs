using Yupi.Model.Domain.Components;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public class LoadBadgesWidgetMessageComposer : AbstractComposer<UserBadgeComponent>
    {
        public override void Compose(ISender session, UserBadgeComponent badges)
        {
        }
    }
}