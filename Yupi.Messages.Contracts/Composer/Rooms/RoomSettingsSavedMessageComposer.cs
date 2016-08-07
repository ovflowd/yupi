using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
	public abstract class RoomSettingsSavedMessageComposer : AbstractComposer<uint>
	{
		public override void Compose(Yupi.Protocol.ISender session, uint roomId)
		{
		 // Do nothing by default.
		}
	}
}
