using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
	public abstract class AddWallItemMessageComposer : AbstractComposer<IWallItem, UserInfo>
	{
		public override void Compose(Yupi.Protocol.ISender session, IWallItem item, UserInfo user)
		{
		 // Do nothing by default.
		}
	}
}
