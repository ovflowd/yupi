using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
	public abstract class ChangeFavouriteGroupMessageComposer : AbstractComposer<Group, int>
	{
		public override void Compose(Yupi.Protocol.ISender session, Group group, int virtualId)
		{
		 // Do nothing by default.
		}
	}
}
