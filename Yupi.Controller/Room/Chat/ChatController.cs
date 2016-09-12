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

		public void Whisper (Habbo session, string message, ChatBubbleStyle bubble, RoomEntity target, int count = -1)
		{
			if (!Validate (ref message) || TryHandleCommand (message) || !bubble.CanUse (session.Info)) {
				return;
			}
				
			session.RoomEntity.Wake();

			ChatlogEntry entry = CreateEntry (session, message, bubble);
			entry.Whisper = true;
			// TODO Save Whisper target
			// TODO Save Chatlog

			session.Router.GetComposer<WhisperMessageComposer> ().Compose (session, entry, count);

			if (target != null) {
				target.HandleChatMessage (session.RoomEntity, targetSession => {
					targetSession.Router.GetComposer<WhisperMessageComposer> ()
						.Compose (targetSession, entry, count);
				});
			}

			// TODO Trigger Wired
		}

		public void Shout (Habbo session, string message, ChatBubbleStyle bubble, int count = -1)
		{
			Chat (session, message, bubble, (user, entry) => { 
				user.Router.GetComposer<ShoutMessageComposer> ()
					.Compose (user, entry, count);
			});
		}

		public void Chat (Habbo session, string message, ChatBubbleStyle bubble, int count = -1)
		{
			Chat (session, message, bubble, (user, entry) => { 
				user.Router.GetComposer<ChatMessageComposer> ()
					.Compose (user, entry, count);
			});
		}

		private ChatlogEntry CreateEntry (Habbo session, string message, ChatBubbleStyle bubble)
		{
			ChatlogEntry entry = new ChatlogEntry (message) {
				User = session.Info,
				Bubble = bubble
			};

			session.Room.Data.Chatlog.Add (entry);
			return entry;
		}

		private void Chat (Habbo session, string message, ChatBubbleStyle bubble, Action<Habbo, ChatlogEntry> composer)
		{
			if (!Validate (ref message) || TryHandleCommand (message) || !bubble.CanUse (session.Info)) {
				return;
			}

			session.RoomEntity.Wake();

			ChatlogEntry entry = CreateEntry (session, message, bubble);

			session.Info.Preferences.ChatBubbleStyle = bubble;
			// TODO Save Preferences
			// TODO Save Chatlog
			session.Room.EachEntity (
				entity => {
					entity.HandleChatMessage (session.RoomEntity, user => composer (user, entry));
				}
			);

			// TODO Trigger Wired
		}
	}
}
