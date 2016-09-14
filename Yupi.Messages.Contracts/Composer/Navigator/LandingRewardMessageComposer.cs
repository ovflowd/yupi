using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
	public abstract class LandingRewardMessageComposer : AbstractComposer<HotelLandingManager, UserInfo>
	{
		public override void Compose(Yupi.Protocol.ISender session, HotelLandingManager manager, UserInfo user)
		{
		 // Do nothing by default.
		}
	}
}
