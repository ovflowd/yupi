using Yupi.Protocol.Buffers;
using Yupi.Model.Domain.Components;

namespace Yupi.Messages.Contracts
{
	public abstract class LoadVolumeMessageComposer : AbstractComposer<UserPreferences>
	{
		public override void Compose(Yupi.Protocol.ISender session, UserPreferences preferences)
		{
		 // Do nothing by default.
		}
	}
}
