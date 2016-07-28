using System;

using Yupi.Protocol.Buffers;

using System.Collections.Generic;
using Yupi.Model.Domain;
using System.Globalization;

namespace Yupi.Messages.Rooms
{
	public class SetRoomUserMessageComposer : AbstractComposer<List<RoomEntity>, bool>
	{
		public override void Compose ( Yupi.Protocol.ISender room, List<RoomEntity> users, bool hasPublicPool = false)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (users.Count);

				foreach (RoomEntity user in users) {
					Serialize (message, user, hasPublicPool);
				}

				room.Send (message);
			}
		}

		public void Compose ( Yupi.Protocol.ISender room, RoomEntity user, bool hasPublicPool = false)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (1);
				Serialize (message, user, hasPublicPool);
				room.Send (message);
			}
		}

		private void Serialize(ServerMessage messageBuffer, RoomEntity entity) {
			if (entity is UserEntity) {
				Serialize (messageBuffer, (UserEntity)entity);
			} else if (entity is PetEntity) {
				Serialize (messageBuffer, (PetEntity)entity);
			} else if (entity is BotEntity) {
				Serialize (messageBuffer, (BotEntity)entity);
			}
		}

		private void Serialize (ServerMessage messageBuffer, UserEntity user)
		{
			Group group = Yupi.GetGame ().GetGroupManager ().GetGroup (GetClient ().GetHabbo ().FavouriteGroup);

			messageBuffer.AppendInteger (user.UserInfo.Id);
			messageBuffer.AppendString (user.UserInfo.UserName);
			messageBuffer.AppendString (user.UserInfo.Motto);
			messageBuffer.AppendString (user.UserInfo.Look);
			messageBuffer.AppendInteger (user.Id);
			messageBuffer.AppendInteger (user.Position.X);
			messageBuffer.AppendInteger (user.Position.Y);
			messageBuffer.AppendString (user.Position.Z.ToString (CultureInfo.InvariantCulture));
			messageBuffer.AppendInteger (0);
			messageBuffer.AppendInteger (1);
			messageBuffer.AppendString (user.UserInfo.Gender.ToLower ());
			messageBuffer.AppendInteger (user.UserInfo.FavouriteGroup.Id);
			messageBuffer.AppendInteger (0);
			messageBuffer.AppendString (user.UserInfo.FavouriteGroup.Name);
			messageBuffer.AppendString ("");
			messageBuffer.AppendInteger (user.UserInfo.AchievementPoints);
			messageBuffer.AppendBool (false);
		}

		private void Serialize (ServerMessage messageBuffer, PetEntity pet)
		{
			messageBuffer.AppendInteger (pet.Id);
			messageBuffer.AppendString (pet.Info.Name);
			messageBuffer.AppendString (pet.Info.Motto);

			if (pet.Info.Type == "pet_monster")
				messageBuffer.AppendString (pet.Info.MoplaBreed.PlantData);
			else if (pet.Info.HaveSaddle == Convert.ToBoolean (2))
				messageBuffer.AppendString (string.Concat (pet.Info.Look.ToLower (), " 3 4 10 0 2 ", pet.Info.PetHair, " ",
					pet.Info.HairDye, " 3 ", pet.Info.PetHair, " ", pet.Info.HairDye));
			else if (pet.Info.HaveSaddle == Convert.ToBoolean (1))
				messageBuffer.AppendString (string.Concat (pet.Info.Look.ToLower (), " 3 2 ", pet.Info.PetHair, " ",
					pet.Info.HairDye, " 3 ", pet.Info.PetHair, " ", pet.Info.HairDye, " 4 9 0"));
			else
				messageBuffer.AppendString (string.Concat (pet.Info.Look.ToLower (), " 2 2 ", pet.Info.PetHair, " ",
					pet.Info.HairDye, " 3 ", pet.Info.PetHair, " ", pet.Info.HairDye));

			messageBuffer.AppendInteger (pet.Id);
			messageBuffer.AppendInteger (pet.Position.X);
			messageBuffer.AppendInteger (pet.Position.Y);
			messageBuffer.AppendString (pet.Position.Z.ToString (CultureInfo.InvariantCulture));
			messageBuffer.AppendInteger (0);
			messageBuffer.AppendInteger (pet.Type);
			messageBuffer.AppendInteger (pet.Info.RaceId);
			messageBuffer.AppendInteger (pet.Info.Owner.Id);
			messageBuffer.AppendString (pet.Info.Owner.UserName);
			messageBuffer.AppendInteger (pet.Info.Type == "pet_monster" ? 0 : 1);
			messageBuffer.AppendBool (pet.Info.HaveSaddle);
			messageBuffer.AppendBool (pet.RidingHorse);
			messageBuffer.AppendInteger (0);
			messageBuffer.AppendInteger (pet.Info.Type == "pet_monster" ? 1 : 0);
			messageBuffer.AppendString (pet.Info.Type == "pet_monster" ? pet.Info.MoplaBreed.GrowStatus : "");
		}

		private void Serialize (ServerMessage messageBuffer, BotEntity bot)
		{
			messageBuffer.AppendInteger (bot.Id);
			messageBuffer.AppendString (bot.Info.Name);
			messageBuffer.AppendString (bot.Info.Motto);
			messageBuffer.AppendString (bot.Info.Look.ToLower ());
			messageBuffer.AppendInteger (bot.Id);
			messageBuffer.AppendInteger (bot.Position.X);
			messageBuffer.AppendInteger (bot.Position.Y);
			messageBuffer.AppendString (bot.Position.Z.ToString (CultureInfo.InvariantCulture));
			messageBuffer.AppendInteger (0);
			messageBuffer.AppendInteger (bot.Type);
			messageBuffer.AppendString (bot.Info.Gender.ToLower ());
			messageBuffer.AppendInteger (bot.Info.Owner.Id);
			messageBuffer.AppendString (bot.Info.Owner.UserName);
			messageBuffer.AppendInteger (5);
			messageBuffer.AppendShort (1);
			messageBuffer.AppendShort (2);
			messageBuffer.AppendShort (3);
			messageBuffer.AppendShort (4);
			messageBuffer.AppendShort (5);
		}
	}
}

