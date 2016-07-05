using System;


using System.Drawing;


namespace Yupi.Messages.Pets
{
	public class HorseMountOnMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			Yupi.Messages.Rooms room = Yupi.GetGame().GetRoomManager().GetRoom(session.GetHabbo().CurrentRoomId);

			RoomUser roomUserByHabbo = room?.GetRoomUserManager().GetRoomUserByHabbo(session.GetHabbo().Id);

			if (roomUserByHabbo == null)
				return;

			uint petId = request.GetUInt32();
			bool flag = request.GetBool();

			RoomUser pet = room.GetRoomUserManager().GetPet(petId);

			if (pet?.PetData == null)
				return;

			if (pet.PetData.AnyoneCanRide == 0 && pet.PetData.OwnerId != roomUserByHabbo.UserId)
			{
				session.SendNotif(Yupi.GetLanguage().GetVar("horse_error_1"));
				return;
			}

			if (flag)
			{
				if (pet.RidingHorse)
				{
					string[] value = PetLocale.GetValue("pet.alreadymounted");

					Random random = new Random();

					pet.Chat(null, value[random.Next(0, value.Length - 1)], false, 0);
				}
				else if (!roomUserByHabbo.RidingHorse)
				{
					pet.Statusses.Remove("sit");
					pet.Statusses.Remove("lay");
					pet.Statusses.Remove("snf");
					pet.Statusses.Remove("eat");
					pet.Statusses.Remove("ded");
					pet.Statusses.Remove("jmp");

					int x = roomUserByHabbo.X, y = roomUserByHabbo.Y;

					room.Send(room.GetRoomItemHandler()
						.UpdateUserOnRoller(pet, new Point(x, y), 0u, room.GetGameMap().SqAbsoluteHeight(x, y)));
					room.GetRoomUserManager().UpdateUserStatus(pet, false);
					room.Send(room.GetRoomItemHandler()
						.UpdateUserOnRoller(roomUserByHabbo, new Point(x, y), 0u,
							room.GetGameMap().SqAbsoluteHeight(x, y) + 1.0));
					room.GetRoomUserManager().UpdateUserStatus(roomUserByHabbo, false);
					pet.ClearMovement();
					roomUserByHabbo.RidingHorse = true;
					pet.RidingHorse = true;
					pet.HorseId = (uint) roomUserByHabbo.VirtualId;
					roomUserByHabbo.HorseId = Convert.ToUInt32(pet.VirtualId);
					roomUserByHabbo.ApplyEffect(77);
					roomUserByHabbo.Z += 1.0;
					roomUserByHabbo.UpdateNeeded = true;
					pet.UpdateNeeded = true;
				}
			}
			else if (roomUserByHabbo.VirtualId == pet.HorseId)
			{
				pet.Statusses.Remove("sit");
				pet.Statusses.Remove("lay");
				pet.Statusses.Remove("snf");
				pet.Statusses.Remove("eat");
				pet.Statusses.Remove("ded");
				pet.Statusses.Remove("jmp");
				roomUserByHabbo.RidingHorse = false;
				roomUserByHabbo.HorseId = 0u;
				pet.RidingHorse = false;
				pet.HorseId = 0u;
				roomUserByHabbo.MoveTo(new Point(roomUserByHabbo.X + 2, roomUserByHabbo.Y + 2));

				roomUserByHabbo.ApplyEffect(-1);
				roomUserByHabbo.UpdateNeeded = true;
				pet.UpdateNeeded = true;
			}
			else
			{
				session.SendNotif(Yupi.GetLanguage().GetVar("horse_error_2"));
				return;
			}
				
			if (session.GetHabbo().Id != pet.PetData.OwnerId)
			{
					Yupi.GetGame().GetAchievementManager().ProgressUserAchievement(session, "ACH_HorseRent", 1);
			}

			router.GetComposer<SerializePetMessageComposer> ().Compose (room, pet);
		}
	}
}

