using System;

using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;
using System.Globalization;

namespace Yupi.Messages.Rooms
{
	public class RoomSpacesMessageComposer : Yupi.Messages.Contracts.RoomSpacesMessageComposer
	{
		

		public override void Compose ( Yupi.Protocol.ISender session, RoomSpacesMessageComposer.RoomSpacesType type, RoomData data)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendString(type.DisplayName);

				if (type == RoomSpacesType.Wallpaper) {
					message.AppendString(data.WallPaper.ToString(CultureInfo.InvariantCulture));
				} else if(type == RoomSpacesType.Floor) {
					message.AppendString(data.Floor.ToString(CultureInfo.InvariantCulture));
				} else {
					message.AppendString(data.LandScape.ToString(CultureInfo.InvariantCulture));
				}

				session.Send (message);
			}
		}
	}
}

