using System;



namespace Yupi.Messages.Rooms
{
	public class StopTypingMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Net.ISession<GameClient> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			Room room = Yupi.GetGame().GetRoomManager().GetRoom(session.GetHabbo().CurrentRoomId);
			RoomUser roomUserByHabbo = room?.GetRoomUserManager().GetRoomUserByHabbo(session.GetHabbo().Id);

			if (roomUserByHabbo == null)
				return;

			router.GetComposer<TypingStatusMessageComposer> ().Compose (session, roomUserByHabbo.VirtualId, false);
		}
	}
}

