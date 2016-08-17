using System;
using Yupi.Model.Domain;
using Yupi.Messages.Contracts;
using Yupi.Util;



namespace Yupi.Messages.User
{
	public class RoomSettingsMuteUserMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			int targetUserId = message.GetInteger ();
			// TODO Refactor
			message.GetUInt32 ();

			uint duration = message.GetUInt32 ();

			Room room = session.Room;
	
			if (room == null
			    || (room.Data.WhoCanBan == 0 && !room.HasOwnerRights (session.Info))
			    || (room.Data.WhoCanBan == 1 && !room.HasRights (session.Info))) {
				return;
			}

			UserEntity targetUser = room.GetEntity (targetUserId) as UserEntity;

			if (targetUser == null || targetUser.UserInfo.Rank >= session.Info.Rank)
				return;

			room.Data.MutedEntities.Add (new RoomMute () {
				Entity = targetUser.UserInfo,
				ExpiresAt = DateTime.Now.AddMinutes (duration)
			});

			targetUser.User.Router.GetComposer<SuperNotificationMessageComposer> ()
				.Compose (targetUser.User, T._("Notice"), string.Format (T._("room_owner_has_mute_user"), duration), "", "", "", 4); 
		}
	}
}

