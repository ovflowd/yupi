using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
	public abstract class PollQuestionsMessageComposer : AbstractComposer<Poll>
	{
		public override void Compose(Yupi.Protocol.ISender session, Poll poll)
		{
		 // Do nothing by default.
		}
	}
}
