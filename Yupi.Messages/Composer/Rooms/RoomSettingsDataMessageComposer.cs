using System;

using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Rooms
{
	public class RoomSettingsDataMessageComposer : Yupi.Messages.Contracts.RoomSettingsDataMessageComposer
	{
		public override void Compose ( Yupi.Protocol.ISender session, RoomData room)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(room.Id);
				message.AppendString(room.Name);
				message.AppendString(room.Description);
				message.AppendInteger((int)room.State);
				message.AppendInteger(room.Category.Id);
				message.AppendInteger(room.UsersMax);
				throw new NotImplementedException ();
				//message.AppendInteger(room.Model.MapSizeX*room.Model.MapSizeY > 200 ? 50 : 25); // TODO Magic number
				message.AppendInteger(room.Tags.Count);

				foreach (string s in room.Tags)
					message.AppendString(s);

				message.AppendInteger(room.TradeState);
				message.AppendInteger(room.AllowPets);
				message.AppendInteger(room.AllowPetsEating);
				message.AppendInteger(room.AllowWalkThrough);
				message.AppendInteger(room.HideWall);
				message.AppendInteger(room.WallThickness);
				message.AppendInteger(room.FloorThickness);
				message.AppendInteger(room.ChatType);
				message.AppendInteger(room.ChatBalloon);
				message.AppendInteger(room.ChatSpeed);
				message.AppendInteger(room.ChatMaxDistance);
				message.AppendInteger(room.ChatFloodProtection > 2 ? 2 : room.ChatFloodProtection);
				message.AppendBool(false); //allow_dyncats_checkbox
				message.AppendInteger(room.WhoCanMute);
				message.AppendInteger(room.WhoCanKick);
				message.AppendInteger(room.WhoCanBan);
				session.Send (message);
			}
		}
	}
}

