using System;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Protocol.Buffers;
using Yupi.Emulator.Game.Rooms;

namespace Yupi.Messages.Notification
{
	public class SuperNotificationMessageComposer : AbstractComposer
	{
		// TODO might be that url default is "event:"
		public void Compose(GameClient session, string title, string content, string url = "", string urlName = "", string unknown = "", int unknown2 = 4) {
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				BuildMessage (message, title, content, url ,urlName, unknown);
				session.Send (message);
			}
		}

		// TODO Should be changed once Room & GameClient implement ISend
		public void Compose(Room room, string title, string content, string url = "", string urlName = "", string unknown = "", int unknown2 = 4) {
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				BuildMessage (message, title, content, url ,urlName, unknown);
				room.Send (message);
			}
		}

		// TODO Refactor
		private void BuildMessage(ServerMessage message, string title, string content, string url, string urlName, string unknown, int unknown2) {
			message.AppendString (unknown);
			message.AppendInteger (unknown2);

			if (unknown2 == 0) {
				return;
			}

			if (unknown2 == 4) {
				message.AppendString ("title");
				message.AppendString (title);
			}
			message.AppendString ("message");
			message.AppendString (content);
			message.AppendString ("linkUrl");
			message.AppendString (url);
			message.AppendString ("linkTitle");
			message.AppendString (urlName);
		}
	}
}

