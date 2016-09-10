using System;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;
using Yupi.Model;
using Yupi.Controller;
using Yupi.Model.Domain.Components;

namespace Yupi.Messages.Encoders
{
	public static class RoomEncoder
	{
		public static void Append(this ServerMessage message, RoomData data) {
			RoomManager manager = DependencyFactory.Resolve<RoomManager> ();
			Room room = manager.GetIfLoaded (data);
			message.AppendInteger (data.Id);
			message.AppendString (data.Name);
			message.AppendInteger (data.Owner.Id);
			message.AppendString (data.Owner.Name);
			message.AppendInteger ((int)data.State);
			message.AppendInteger (room == null ? 0 : room.GetUserCount());
			message.AppendInteger (data.UsersMax);
			message.AppendString (data.Description);
			message.AppendInteger (data.TradeState);
			message.AppendInteger (data.Score); // Score
			message.AppendInteger (data.Score); // Ranking Difference?
			message.AppendInteger (data.Category.Id);
			message.AppendInteger (data.Tags.Count);

			foreach (string tag in data.Tags) {
				message.AppendString (tag);
			}

			RoomFlags flags = data.GetFlags ();

			message.AppendInteger ((int)flags);

			if ((flags & RoomFlags.Image) > 0) {
				message.AppendString (data.NavigatorImage);
			}

			if ((flags & RoomFlags.Group) > 0) {
				message.AppendInteger (data.Group.Id);
				message.AppendString (data.Group.Name);
				message.AppendString (data.Group.Badge);
			}

			if ((flags & RoomFlags.Event) > 0) {
				message.AppendString (data.Event.Name);
				message.AppendString (data.Event.Description);
				message.AppendInteger ((int)(data.Event.ExpiresAt - DateTime.Now).TotalMinutes);
			}
		}
	
		public static void Append(this ServerMessage message, RoomChatSettings chat) {
			message.AppendInteger(chat.Type);
			message.AppendInteger(chat.Balloon);
			message.AppendInteger(chat.Speed);
			message.AppendInteger(chat.MaxDistance);
			message.AppendInteger(chat.FloodProtection);
		}
	}
}

