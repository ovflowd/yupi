namespace Yupi.Messages.User
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Yupi.Model.Domain;
    using Yupi.Model.Domain.Components;
    using Yupi.Protocol.Buffers;

    public class LoadBadgesWidgetMessageComposer : Contracts.LoadBadgesWidgetMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, UserBadgeComponent badges)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(badges.Badges.Count);

                foreach (Badge badge in badges.Badges)
                {
                    message.AppendInteger(1); // TODO Magic constant
                    message.AppendString(badge.Code);
                }

                IList<Badge> visibleBadges = badges.GetVisible();

                message.AppendInteger(visibleBadges.Count);

                foreach (Badge badge in visibleBadges)
                {
                    message.AppendInteger(badge.Slot);
                    message.AppendString(badge.Code);
                }

                session.Send(message);
            }
        }

        #endregion Methods
    }
}