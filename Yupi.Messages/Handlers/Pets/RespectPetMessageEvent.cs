using System;

namespace Yupi.Messages.Pets
{
	public class RespectPetMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Net.ISession<IGameClient> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			Room room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);

			if (room == null)
				return;

			uint petId = Request.GetUInt32();

			RoomUser pet = room.GetRoomUserManager().GetPet(petId);

			if (pet?.PetData == null)
				return;

			pet.PetData.OnRespect();

			if (pet.PetData.Type == "pet_monster")
				Yupi.GetGame().GetAchievementManager().ProgressUserAchievement(Session, "ACH_MonsterPlantTreater", 1);
			else
			{
				Session.GetHabbo().DailyPetRespectPoints--;

				Yupi.GetGame().GetAchievementManager().ProgressUserAchievement(Session, "ACH_PetRespectGiver", 1);

				string[] value = PetLocale.GetValue("pet.respected");
				string message = value[new Random().Next(0, value.Length - 1)];

				pet.Chat(null, message, false, 0);

				using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
					queryReactor.RunFastQuery(
						$"UPDATE users_stats SET daily_pet_respect_points = daily_pet_respect_points - 1 WHERE id = {Session.GetHabbo().Id} LIMIT 1");
			}
		}
	}
}

