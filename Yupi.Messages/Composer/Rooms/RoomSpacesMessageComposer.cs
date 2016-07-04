using System;
using Yupi.Emulator.Game.Rooms.Data.Models;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Rooms
{
	public class RoomSpacesMessageComposer : AbstractComposer<RoomSpacesMessageComposer.Type, RoomData>
	{
		public enum Type
		{
			WALLPAPER,
			FLOOR,
			LANDSCAPE
		}

		public override void Compose (Yupi.Protocol.ISender session, RoomSpacesMessageComposer.Type type, RoomData data)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendString(type.ToString());

				// TODO use enum
				switch (type) {
				case Type.WALLPAPER:
					message.AppendString(data.WallPaper);
					break;
				case Type.FLOOR:
					message.AppendString(data.Floor);
					break;
				case Type.LANDSCAPE:
					message.AppendString(data.LandScape);
					break;
				}

				session.Send (message);
			}
		}
	}
}

