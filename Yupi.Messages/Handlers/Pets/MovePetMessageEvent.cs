using System;
using Yupi.Emulator.Game.Rooms.User;
using Yupi.Emulator.Game.Pets.Enums;
using System.Drawing;

namespace Yupi.Messages.Pets
{
	public class MovePetMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			Yupi.Messages.Rooms room = Yupi.GetGame().GetRoomManager().GetRoom(session.GetHabbo().CurrentRoomId);

			if ((room == null) || !room.CheckRights(session))
			{
				session.SendNotif(Yupi.GetLanguage().GetVar("monsterplant_error_6"));
				return;
			}

			uint petId = request.GetUInt32();

			RoomUser pet = room.GetRoomUserManager().GetPet(petId);

			if (pet == null || !pet.IsPet || pet.PetData.Type != "pet_monster")
			{
				session.SendNotif(Yupi.GetLanguage().GetVar("monsterplant_error_7"));
				return;
			}

			int x = request.GetInteger();
			int y = request.GetInteger();
			int rot = request.GetInteger();
			int oldX = pet.X;
			int oldY = pet.Y;

			if ((x != oldX) && (y != oldY))
			{
				if (!room.GetGameMap().CanWalk(x, y, false))
				{
					session.SendNotif(Yupi.GetLanguage().GetVar("monsterplant_error_8"));
					return;
				}
			}

			if ((rot < 0) || (rot > 6) || (rot%2 != 0))
				rot = pet.RotBody;

			pet.PetData.X = x;
			pet.PetData.Y = y;
			pet.X = x;
			pet.Y = y;
			pet.RotBody = rot;
			pet.RotHead = rot;

			if (pet.PetData.DbState != DatabaseUpdateState.NeedsInsert)
				pet.PetData.DbState = DatabaseUpdateState.NeedsUpdate;

			pet.UpdateNeeded = true;
			room.GetGameMap().UpdateUserMovement(new Point(oldX, oldY), new Point(x, y), pet);
		}
	}
}

