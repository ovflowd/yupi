using System;
using Yupi.Model.Domain;
using Yupi.Model;



namespace Yupi.Messages.Chat
{
	public class ChatMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			if (session.RoomEntity == null)
				return;

			string message = request.GetString ();

			// TODO TO Enum!
			int bubbleId = request.GetInteger ();

			// Client side message id
			int count = request.GetInteger ();

			// TODO Magic constant
			if (message.Length > 100) {
				return;
			}

			ChatBubbleStyle bubble;

			if (ChatBubbleStyle.TryFromInt32 (bubbleId, out bubble)) {
				/* TODO Implement
				if (!ServerSecurityChatFilter.CanTalk(session, msg))
					return;
					*/

				// TODO Wordfilter
				// TODO Flood
				// TODO Room Mute

				// TODO Implement
				// session.RoomEntity.UnIdle();

				/* TODO Command manager
				 * if (msg.StartsWith(":") && CommandsManager.TryExecute(msg.Substring(1), session))
					return;
				*/

				session.Room.Data.Chatlog.Add (new ChatlogEntry () {
					Message = message,
					Timestamp = DateTime.Now,
					User = session.Info
				});

				// TODO Save Chatlog

				session.Info.Preferences.ChatBubbleStyle = bubble;
				// TODO Save Preferences

				session.Room.EachUser (roomSession => {
					// TODO Implement Tent
					if (!roomSession.Info.MutedUsers.Contains (session.Info)) {
						
					}
				});

				session.Room.EachEntity (entity => {
					if (entity is UserEntity) {
						Habbo user = ((UserEntity)entity).User;
						if (user.Info.MutedUsers.Contains (session.Info)) {
							return;
						} else {
							user.Router.GetComposer<ChatMessageComposer> ()
								.Compose (user, session.RoomEntity.Id, message, bubble, count);
						}
					}

					if (entity != session.RoomEntity && entity.Type != EntityType.Pet) {
						int rotation = entity.Position.CalculateRotation (session.RoomEntity.Position);
						// TODO Should only be temporary
						// TODO Add distance calculation!
						entity.SetHeadRotation (rotation);
					}
				});

				// TODO Trigger Wired + Bots
			}
		}
	}
}

