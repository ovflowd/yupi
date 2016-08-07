using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
	public abstract class GroupConfirmLeaveMessageComposer : AbstractComposer<UserInfo, Group, int>
	{
		public override void Compose(Yupi.Protocol.ISender session, UserInfo user, Group group, int type)
		{
		 // Do nothing by default.
		}
	}
}
