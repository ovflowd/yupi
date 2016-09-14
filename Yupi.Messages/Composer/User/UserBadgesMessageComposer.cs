namespace Yupi.Messages.User
{
    using System;
    using System.Linq;

    using Yupi.Model.Domain;
    using Yupi.Net;
    using Yupi.Protocol.Buffers;

    public class UserBadgesMessageComposer : Yupi.Messages.Contracts.UserBadgesMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, UserInfo user)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(user.Id);
                var badges = user.Badges.GetVisible();

                message.AppendInteger(badges.Count);

                foreach (Badge badge in badges)
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