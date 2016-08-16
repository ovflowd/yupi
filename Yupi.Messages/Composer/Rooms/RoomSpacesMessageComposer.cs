using System;

using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Rooms
{
	public class RoomSpacesMessageComposer : Yupi.Messages.Contracts.RoomSpacesMessageComposer
	{
		

		public override void Compose ( Yupi.Protocol.ISender session, RoomSpacesMessageComposer.RoomSpacesType type, RoomData data)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendString(type.DisplayName);

				if (type == RoomSpacesType.Wallpaper) {
					message.AppendString(data.WallPaper);
				} else if(type == RoomSpacesType.Floor) {
					message.AppendString(data.Floor);
				} else {
					message.AppendString(data.LandScape);
				}

				session.Send (message);
			}
		}
	}
}

