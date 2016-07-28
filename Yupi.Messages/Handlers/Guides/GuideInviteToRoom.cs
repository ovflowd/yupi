using System;



namespace Yupi.Messages.Guides
{
	public class GuideInviteToRoom : AbstractHandler
	{
		public override void HandleMessage (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			GameClient requester = session.GetHabbo().GuideOtherUser;

			Yupi.Messages.Rooms room = session.GetHabbo().CurrentRoom;

			if (room == null) {
				return;
			}

			router.GetComposer<OnGuideSessionInvitedToGuideRoomMessageComposer> ().Compose (requester, room.RoomId, room.RoomData.Name);

			// TODO Is this really required
			router.GetComposer<OnGuideSessionInvitedToGuideRoomMessageComposer> ().Compose (session, room.RoomId, room.RoomData.Name);
		}
	}
}

