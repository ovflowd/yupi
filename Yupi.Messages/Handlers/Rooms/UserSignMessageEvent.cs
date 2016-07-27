using System;



namespace Yupi.Messages.Rooms
{
	public class UserSignMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Net.ISession<IGameClient> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			Room room = Yupi.GetGame().GetRoomManager().GetRoom(session.GetHabbo().CurrentRoomId);
			RoomUser roomUserByHabbo = room?.GetRoomUserManager().GetRoomUserByHabbo(session.GetHabbo().Id);

			if (roomUserByHabbo == null)
				return;

			roomUserByHabbo.UnIdle();

			// TODO Should this value be verified?
			int value = request.GetInteger();

			roomUserByHabbo.AddStatus("sign", Convert.ToString(value));
			roomUserByHabbo.UpdateNeeded = true;
			roomUserByHabbo.SignTime = Yupi.GetUnixTimeStamp() + 5; // TODO Why +5
		}
	}
}

