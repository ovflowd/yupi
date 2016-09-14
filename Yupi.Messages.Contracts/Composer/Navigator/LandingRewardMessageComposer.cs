using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class LandingRewardMessageComposer : AbstractComposer<HotelLandingManager, UserInfo>
    {
        public override void Compose(ISender session, HotelLandingManager manager, UserInfo user)
        {
            // Do nothing by default.
        }
    }
}