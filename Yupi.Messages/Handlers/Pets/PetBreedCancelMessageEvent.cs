using System;




using System.Drawing;

namespace Yupi.Messages.Pets
{
	public class PetBreedCancelMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			Yupi.Messages.Rooms room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);

			if (room == null || !room.CheckRights(session, true))
				return;

			uint itemId = request.GetUInt32();

			RoomItem item = room.GetRoomItemHandler().GetItem(itemId);

			if (item == null)
				return;

			if (item.GetBaseItem().InteractionType != Interaction.BreedingTerrier &&
				item.GetBaseItem().InteractionType != Interaction.BreedingBear)
				return;

			foreach (Pet pet in item.PetsList)
			{
				pet.WaitingForBreading = 0;
				pet.BreadingTile = new Point();

				RoomUser user = room.GetRoomUserManager().GetRoomUserByVirtualId(pet.VirtualId);
				user.Freezed = false;
				room.GetGameMap().AddUserToMap(user, user.Coordinate);

				Point nextCoord = room.GetGameMap().GetRandomValidWalkableSquare();
				user.MoveTo(nextCoord.X, nextCoord.Y);
			}

			item.PetsList.Clear();
		}
	}
}

