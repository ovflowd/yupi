using System;
using Yupi.Emulator.Game.Items.Interfaces;
using Yupi.Emulator.Game.Items.Interactions.Enums;
using System.Collections.Generic;
using System.Drawing;
using Yupi.Emulator.Game.Pets;
using Yupi.Emulator.Game.Rooms.User;
using Yupi.Emulator.Data.Base.Adapters.Interfaces;

namespace Yupi.Messages.Items
{
	public class FloorItemMoveMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			uint id = request.GetUInt32();

			Yupi.Messages.Rooms room = Yupi.GetGame().GetRoomManager().GetRoom(session.GetHabbo().CurrentRoomId);

			if (room == null)
				return;

			if (!room.CheckRights(session, false, true))
				return;

			RoomItem item = room.GetRoomItemHandler().GetItem(id);

			if (item == null)
				return;

			int x = request.GetInteger();
			int y = request.GetInteger();
			int rot = request.GetInteger();
			// TODO Unused
			request.GetInteger();

			bool flag = item.GetBaseItem().InteractionType == Interaction.Teleport ||
				item.GetBaseItem().InteractionType == Interaction.Hopper ||
				item.GetBaseItem().InteractionType == Interaction.QuickTeleport;

			List<Point> oldCoords = item.GetCoords;

			if (!room.GetRoomItemHandler().SetFloorItem(session, item, x, y, rot, false, false, true, true, false))
			{
				router.GetComposer<UpdateRoomItemMessageComposer> ().Compose (room, item);
				return;
			}

			if (item.GetBaseItem().InteractionType == Interaction.BreedingTerrier ||
				item.GetBaseItem().InteractionType == Interaction.BreedingBear)
			{
				foreach (Pet pet in item.PetsList)
				{
					pet.WaitingForBreading = 0;
					pet.BreadingTile = new Point();

					RoomUser user = room.GetRoomUserManager().GetRoomUserByVirtualId(pet.VirtualId);

					if (user == null)
						continue;

					user.Freezed = false;
					room.GetGameMap().AddUserToMap(user, user.Coordinate);

					Point nextCoord = room.GetGameMap().GetRandomValidWalkableSquare();
					user.MoveTo(nextCoord.X, nextCoord.Y);
				}

				item.PetsList.Clear();
			}

			List<Point> newcoords = item.GetCoords;
			room.GetRoomItemHandler().OnHeightMapUpdate(oldCoords, newcoords);

			if (!flag)
				return;

			using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
				room.GetRoomItemHandler().SaveFurniture(queryReactor);
		}
	}
}

