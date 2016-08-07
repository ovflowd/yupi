using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
	public abstract class GroupDataEditMessageComposer : AbstractComposer<Group>
	{
		public override void Compose(Yupi.Protocol.ISender session, Group group)
		{
		 // Do nothing by default.
		}
	}
}
