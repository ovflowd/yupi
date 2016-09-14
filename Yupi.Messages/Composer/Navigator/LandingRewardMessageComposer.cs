using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Navigator
{
    public class LandingRewardMessageComposer : Contracts.LandingRewardMessageComposer
    {
        public override void Compose(ISender session, HotelLandingManager manager, UserInfo user)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendString(manager.FurniReward.Name);
                message.AppendInteger(manager.FurniReward.Id);
                message.AppendInteger(120); // TODO Magic constant
                message.AppendInteger(120 - user.Respect.Respect);
                session.Send(message);
            }
        }
    }
}