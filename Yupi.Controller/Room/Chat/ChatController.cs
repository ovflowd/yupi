using System;
using Yupi.Model.Domain;
using Yupi.Messages.Contracts;
using Yupi.Model;

namespace Yupi.Controller
{
	public class ChatController
	{
		public const int MAX_MESSAGE_LENGTH = 100;

		private bool Validate (ref string message)
		{
			if (message.Length > MAX_MESSAGE_LENGTH) {
				return false;
			}

			/* TODO Implement
				if (!ServerSecurityChatFilter.CanTalk(session, msg))
					return false;
					*/

			// TODO Wordfilter
			// TODO Flood
			// TODO Room Mute

			return true;
		}

		private bool TryHandleCommand (string message)
		{
			/* TODO Command manager
				 * return msg.StartsWith(":") && CommandsManager.TryExecute(msg.Substring(1), session)
				*/
			return false;
		}

		public void Shout (Habbo session, string message, ChatBubbleStyle bubble, int count = -1)
		{
			/*Chat (session, message, bubble, (user, entry) => { 
				user.Router.GetComposer<ShoutMessageComposer> ()
					.Compose (user, entry, count);
			});*/
		}

		public void Chat (Habbo session, string message, ChatBubbleStyle bubble, int count = -1)
		{
			Chat (session, message, bubble, (user, entry) => { 
				user.Router.GetComposer<ChatMessageComposer> ()
					.Compose (user, entry, count);
			});
		}

		private void Chat (Habbo session, string message, ChatBubbleStyle bubble, Action<Habbo, ChatlogEntry> composer)
		{
			if (!Validate (ref message) || TryHandleCommand (message)) {
				return;
			}

			// TODO Implement
			// session.RoomEntity.UnIdle();

			ChatlogEntry entry = new ChatlogEntry (message) {
				User = session.Info,
				Bubble = bubble
			};

			session.Room.Data.Chatlog.Add (entry);

			// TODO Save Chatlog

			session.Info.Preferences.ChatBubbleStyle = bubble;
			// TODO Save Preferences

			session.Room.EachEntity (
				entity => {
					entity.HandleChatMessage (session.RoomEntity, user => composer(user, entry));
				}
			);

			// TODO Trigger Wired
		}
	}
}
