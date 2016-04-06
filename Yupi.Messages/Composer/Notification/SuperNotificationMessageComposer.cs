using System;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Notification
{
	public class SuperNotificationMessageComposer : AbstractComposer
	{
		// TODO might be that url default is "event:"
		public void Compose(GameClient session, string title, string content, string url = "", string urlName = "") {
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendString (string.Empty);
				message.AppendInteger (4); // TODO What does 4 mean?
				message.AppendString ("title");
				message.AppendString (title);
				message.AppendString ("message");
				message.AppendString (content);
				message.AppendString ("linkUrl");
				message.AppendString (url);
				message.AppendString ("linkTitle");
				message.AppendString (urlName);

				session.Send (message);
			}
		}
	}
}

