using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
	public abstract class OnGuideSessionMsgMessageComposer : AbstractComposer<Yupi.Protocol.ISender, string, int>
	{
		public override void Compose(Yupi.Protocol.ISender session,  Yupi.Protocol.ISender requester, string content, int userId)
		{
		 // Do nothing by default.
		}
	}
}
