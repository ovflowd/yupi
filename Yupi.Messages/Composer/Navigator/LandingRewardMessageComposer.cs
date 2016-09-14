namespace Yupi.Messages.Navigator
{
    using System;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public class LandingRewardMessageComposer : Yupi.Messages.Contracts.LandingRewardMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, HotelLandingManager manager, UserInfo user)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendString(manager.FurniReward.Name);
                message.AppendInteger(manager.FurniReward.Id);
                message.AppendInteger(120); // TODO Magic constant
                message.AppendInteger(120 - user.Respect.Respect);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}