using System;



namespace Yupi.Messages.Rooms
{
	public class SetHomeRoomMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			uint roomId = request.GetUInt32();

			/*
			RoomData data = Yupi.GetGame().GetRoomManager().GenerateRoomData(roomId);

			if (roomId != 0 && data == null)
			{
				session.GetHabbo().HomeRoom = roomId;

				using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
					queryReactor.RunFastQuery(string.Concat("UPDATE users SET home_room = ", roomId,
						" WHERE id = ", session.GetHabbo().Id));

				router.GetComposer<HomeRoomMessageComposer> ().Compose (session, roomId);
			}
			*/
			throw new NotImplementedException ();
		}
	}
}

