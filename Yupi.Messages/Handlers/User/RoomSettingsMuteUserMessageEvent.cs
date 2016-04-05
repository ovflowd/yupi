using System;
using Yupi.Emulator.Game.Rooms;
using Yupi.Emulator.Game.GameClients.Interfaces;

namespace Yupi.Messages.User
{
	public class RoomSettingsMuteUserMessageEvent : AbstractHandler
	{
		public override void HandleMessage (GameClient session, Yupi.Protocol.Buffers.ClientMessage message, Router router)
		{
			uint num = message.GetUInt32();
			// TODO Refactor
			message.GetUInt32();

			uint num2 = message.GetUInt32();

			Room currentRoom = Session.GetHabbo().CurrentRoom;

			if ((currentRoom == null || (currentRoom.RoomData.WhoCanBan == 0 && !currentRoom.CheckRights(session.UserData, true)) ||
				(currentRoom.RoomData.WhoCanBan == 1 && !currentRoom.CheckRights(session.UserData))) &&
				session.UserData.GetHabbo().Rank < Convert.ToUInt32(Yupi.GetDbConfig().DbData["ambassador.minrank"]))
				return;

			RoomUser roomUserByHabbo = currentRoom?.GetRoomUserManager().GetRoomUserByHabbo(Yupi.GetHabboById(num).UserName);

			if (roomUserByHabbo == null)
				return;

			if (roomUserByHabbo.GetClient().GetHabbo().Rank >= Session.GetHabbo().Rank)
				return;

			if (currentRoom.MutedUsers.ContainsKey(num))
			{
				if (currentRoom.MutedUsers[num] >= (ulong) Yupi.GetUnixTimeStamp())
					return;
				currentRoom.MutedUsers.Remove(num);
			}

			currentRoom.MutedUsers.Add(num, uint.Parse((Yupi.GetUnixTimeStamp() + checked(num2*60u)).ToString()));

			roomUserByHabbo.GetClient()
				.SendNotif(string.Format(Yupi.GetLanguage().GetVar("room_owner_has_mute_user"), num2));
		}
	}
}

