using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
	public abstract class GroupForumNewResponseMessageComposer : AbstractComposer
	{
		public virtual void Compose(Yupi.Protocol.ISender session, int groupId, int threadId, int messageCount, UserInfo user, int timestamp, string content)
		{
		 // Do nothing by default.
		}
	}
}
