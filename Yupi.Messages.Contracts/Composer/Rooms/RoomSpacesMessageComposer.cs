using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
	public abstract class RoomSpacesMessageComposer : AbstractComposer<RoomSpacesMessageComposer.Type, RoomData>
	{
		public enum Type
		{
			WALLPAPER,
			FLOOR,
			LANDSCAPE
		}

		public override void Compose(Yupi.Protocol.ISender session, RoomSpacesMessageComposer.Type type, RoomData data)
		{
		 // Do nothing by default.
		}
	}
}
