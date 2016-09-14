using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
	public abstract class ModerationToolIssueMessageComposer : AbstractComposer<SupportTicket>
	{
		public override void Compose(Yupi.Protocol.ISender session, SupportTicket ticket)
		{
		 // Do nothing by default.
		}
	}
}
