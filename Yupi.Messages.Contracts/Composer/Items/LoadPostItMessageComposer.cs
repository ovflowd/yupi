using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
	public abstract class LoadPostItMessageComposer : AbstractComposer<PostItItem>
	{
		public override void Compose(Yupi.Protocol.ISender session, PostItItem item)
		{
		 // Do nothing by default.
		}
	}
}
