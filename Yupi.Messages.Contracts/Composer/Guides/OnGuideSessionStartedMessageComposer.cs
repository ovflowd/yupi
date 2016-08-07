using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
	public abstract class OnGuideSessionStartedMessageComposer : AbstractComposer<UserInfo,  Yupi.Protocol.ISender>
	{
		public override void Compose(Yupi.Protocol.ISender session, UserInfo habbo,  Yupi.Protocol.ISender requester)
		{
		 // Do nothing by default.
		}
	}
}
