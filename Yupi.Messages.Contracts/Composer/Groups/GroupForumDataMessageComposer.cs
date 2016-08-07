using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
	public abstract class GroupForumDataMessageComposer : AbstractComposer<Group, uint>
	{
		public override void Compose(Yupi.Protocol.ISender session, Group group, uint userId)
		{
		 // Do nothing by default.
		}
	}
}
