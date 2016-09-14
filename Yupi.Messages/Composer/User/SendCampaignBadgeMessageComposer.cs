namespace Yupi.Messages.User
{
    using System;

    using Yupi.Protocol.Buffers;

    public class SendCampaignBadgeMessageComposer : Yupi.Messages.Contracts.SendCampaignBadgeMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, string badgeName, bool hasBadge)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendString(badgeName);
                message.AppendBool(hasBadge);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}