using System;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;
using Yupi.Controller;
using Yupi.Model;
using Yupi.Util;


namespace Yupi.Messages.Chat
{
	public class ChatMessageComposer : Yupi.Messages.Contracts.ChatMessageComposer
	{
		public override void Compose (Yupi.Protocol.ISender session, ChatlogEntry msg, int count = -1)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (msg.User.Id);
				message.AppendString (msg.FilteredMessage());
				message.AppendInteger ((int)msg.GetEmotion());
				message.AppendInteger (msg.Bubble.Value);

				// Replaces placeholders the way String.Format does: {0}
				message.AppendInteger (msg.Links.Count);

				foreach (Link link in msg.Links) {
					message.AppendString (link.URL);
					message.AppendString (link.Text);
					message.AppendBool (link.IsInternal);
				}

				// Count is used to detect lag (client side)
				message.AppendInteger (count);
				session.Send (message);
			}
		}
	}
}

