using System;
using System.Collections.Generic;

namespace Yupi.Messages.User
{
	public class FindMoreFriendsMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage message, Router router)
		{
			KeyValuePair<RoomData, uint>[] allRooms = Yupi.GetGame().GetRoomManager().GetActiveRooms();

			if (allRooms != null) {
				Random rnd = new Random();
				RoomData randomRoom = allRooms[rnd.Next(allRooms.Length)].Key;

				router.GetComposer<FindMoreFriendsSuccessMessageComposer> ().Compose (session, randomRoom != null);

				if (randomRoom != null) {
					router.GetComposer<RoomForwardMessageComposer> ().Compose (session, randomRoom.Id);
				}
			}
		}
	}
}

