using System;
using Yupi.Model.Domain;
using Yupi.Messages.Contracts;
using Yupi.Util;



namespace Yupi.Messages.User
{
	public class RoomSettingsMuteUserMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			int targetUserId = message.GetInteger ();
			// TODO Refactor
			message.GetUInt32 ();

			uint duration = message.GetUInt32 ();

			Room room = session.UserData.Room;
	
			if (room == null
			    || (room.Data.WhoCanBan == 0 && !room.HasOwnerRights (session.UserData.Info))
			    || (room.Data.WhoCanBan == 1 && !room.HasRights (session.UserData.Info))) {
				return;
			}

			UserEntity targetUser = room.GetEntity (targetUserId) as UserEntity;

			if (targetUser == null || targetUser.UserInfo.Rank >= session.UserData.Info.Rank)
				return;

			room.Data.MutedEntities.Add (new RoomMute () {
				Entity = targetUser.UserInfo,
				ExpiresAt = DateTime.Now.AddMinutes (duration)
			});

			targetUser.User.Session.Router.GetComposer<SuperNotificationMessageComposer> ()
				.Compose (targetUser.User, T._("Notice"), string.Format (T._("room_owner_has_mute_user"), duration), "", "", "", 4); 
		}
	}
}

