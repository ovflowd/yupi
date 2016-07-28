using System;
using System.Collections.Generic;

using System.Linq;
using Yupi.Messages.User;

namespace Yupi.Messages.Other
{
	public class GoToRoomByNameMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			string name = request.GetString();

			uint roomId = 0;

			// TODO Refactor
			switch (name)
			{
			case "predefined_noob_lobby":
				{
					roomId = Convert.ToUInt32(Yupi.GetDbConfig().DbData["noob.lobby.roomid"]);

					break;
				}
			case "random_friending_room":
				{
					if (Yupi.GetGame().GetRoomManager().GetActiveRooms() == null)
						return;

					List<RoomData> rooms = Yupi.GetGame().GetRoomManager().GetActiveRooms().Select(room => room.Key).Where(room => room != null && room.UsersNow > 0).ToList();

					if (!rooms.Any())
						return;

					if (rooms.Count == 1)
					{
						roomId = rooms.First().Id;

						break;
					}

					roomId = rooms[Yupi.GetRandomNumber(0, rooms.Count)].Id;

					break;
				}
			}

			if (roomId == 0)
				return;

			router.GetComposer<RoomForwardMessageComposer> ().Compose (session, roomId);
		}
	}
}

