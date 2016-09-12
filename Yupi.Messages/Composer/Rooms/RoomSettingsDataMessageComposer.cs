using System;

using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;
using Yupi.Messages.Encoders;

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
				message.AppendInteger(room.State.Value);
				message.AppendInteger(room.Category.Id);
				message.AppendInteger(room.UsersMax);
				message.AppendInteger(0); // unused
				message.AppendInteger(room.Tags.Count);

				foreach (string s in room.Tags)
					message.AppendString(s);

				message.AppendInteger(room.TradeState.Value);
				message.AppendInteger(room.AllowPets);
				message.AppendInteger(room.AllowPetsEating);
				message.AppendInteger(room.AllowWalkThrough);
				message.AppendInteger(room.HideWall);
				message.AppendInteger(room.WallThickness);
				message.AppendInteger(room.FloorThickness);
				message.Append(room.Chat);
				message.AppendBool(false); //TODO allow_dyncats_checkbox
				message.Append(room.ModerationSettings);
				session.Send (message);
			}
		}
	}
}

