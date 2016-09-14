using System;
using Yupi.Model.Domain;
using System.Collections.Generic;
using Yupi.Model.Domain.Components;

namespace Yupi.Messages.Contracts
{
    public class LoadBadgesWidgetMessageComposer : AbstractComposer<UserBadgeComponent>
    {
        public override void Compose(Yupi.Protocol.ISender session, UserBadgeComponent badges)
        {
        }
    }
}