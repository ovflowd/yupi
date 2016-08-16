using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;
using Headspring;

namespace Yupi.Messages.Contracts
{
	public abstract class RoomSpacesMessageComposer : AbstractComposer<RoomSpacesMessageComposer.RoomSpacesType, RoomData>
	{
		public class RoomSpacesType : Enumeration<RoomSpacesType>
		{
			public static readonly RoomSpacesType Wallpaper = new RoomSpacesType(0, "wallpaper");
			public static readonly RoomSpacesType Floor = new RoomSpacesType(1, "floor");
			public static readonly RoomSpacesType Landscape = new RoomSpacesType(2, "landscape");

			private RoomSpacesType (int value, string displayName) : base(value, displayName)
			{
				
			}
		}

		public override void Compose(Yupi.Protocol.ISender session, RoomSpacesMessageComposer.RoomSpacesType type, RoomData data)
		{
		 // Do nothing by default.
		}
	}
}
