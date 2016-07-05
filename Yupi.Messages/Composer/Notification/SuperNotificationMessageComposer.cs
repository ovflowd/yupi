using System;

using Yupi.Protocol.Buffers;


namespace Yupi.Messages.Notification
{
	public class SuperNotificationMessageComposer : AbstractComposer
	{
		// TODO might be that url default is "event:"
		// unknown might be icon id!
		public void Compose(GameClient session, string title, string content, string url = "", string urlName = "", string unknown = "", int unknown2 = 4) {
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendString (unknown);
				message.AppendInteger (unknown2);
				// TODO Refactor
				if (unknown2 == 0) {
					return;
				}

				switch (unknown2) {
				case 0:
					// nothing more to do
					break;
				case 1:
					message.AppendString ("errors");
					message.AppendString (content);
					break;
				case 2:
					message.AppendString("link");
					message.AppendString("event:");
					message.AppendString("linkTitle");
					message.AppendString("ok");
					break;
				case 4:
					message.AppendString ("title");
					message.AppendString (title);
					break;
				}

				if (unknown2 == 3 || unknown2 == 4) {
					message.AppendString ("message");
					message.AppendString (content);
					message.AppendString ("linkUrl");
					message.AppendString (url);
					message.AppendString ("linkTitle");
					message.AppendString (urlName);
				}

				session.Send (message);
			}
		}
	}
}

