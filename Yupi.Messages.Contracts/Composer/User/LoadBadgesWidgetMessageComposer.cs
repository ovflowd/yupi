namespace Yupi.Messages.Contracts
{
    using System;
    using System.Collections.Generic;

    using Yupi.Model.Domain;
    using Yupi.Model.Domain.Components;

    public class LoadBadgesWidgetMessageComposer : AbstractComposer<UserBadgeComponent>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, UserBadgeComponent badges)
        {
        }

        #endregion Methods
    }
}