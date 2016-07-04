using System;
using Yupi.Emulator.Game.Groups.Structs;
using Yupi.Emulator.Game.GameClients.Interfaces;
using System.Collections.Generic;
using Yupi.Emulator.Game.Items.Interfaces;
using Yupi.Emulator.Game.Rooms.Data.Models;
using Yupi.Emulator.Data.Base.Adapters.Interfaces;

namespace Yupi.Messages.Groups
{
	public class DeleteGroupMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			uint groupId = request.GetUInt32();

			Group group = Yupi.GetGame().GetGroupManager().GetGroup(groupId);
			Yupi.Messages.Rooms room = Yupi.GetGame().GetRoomManager().GetRoom(group.RoomId);

			if (room?.RoomData?.Group == null)
				session.SendNotif(Yupi.GetLanguage().GetVar("command_group_has_no_room"));
			else
			{
				// TODO Refactor?
				foreach (GroupMember user in group.Members.Values)
				{
					GameClient clientByUserId = Yupi.GetGame().GetClientManager().GetClientByUserId(user.Id);

					if (clientByUserId == null)
						continue;

					clientByUserId.GetHabbo().UserGroups.Remove(user);

					if (clientByUserId.GetHabbo().FavouriteGroup == group.Id)
						clientByUserId.GetHabbo().FavouriteGroup = 0;
				}

				room.RoomData.Group = null;
				room.RoomData.GroupId = 0;

				Yupi.GetGame().GetGroupManager().DeleteGroup(@group.Id);

				router.GetComposer<GroupDeletedMessageComposer> ().Compose (room, groupId);

				List<RoomItem> roomItemList = room.GetRoomItemHandler().RemoveAllFurniture(session);
				room.GetRoomItemHandler().RemoveItemsByOwner(ref roomItemList, ref session);
				RoomData roomData = room.RoomData;
				uint roomId = room.RoomData.Id;

				Yupi.GetGame().GetRoomManager().UnloadRoom(room, "Delete room");
				Yupi.GetGame().GetRoomManager().QueueVoteRemove(roomData);

				using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
				{
					// TODO Use parameters (or ORM)
					queryReactor.RunFastQuery($"DELETE FROM rooms_data WHERE id = {roomId}");
					queryReactor.RunFastQuery($"DELETE FROM users_favorites WHERE room_id = {roomId}");
					queryReactor.RunFastQuery($"DELETE FROM items_rooms WHERE room_id = {roomId}");
					queryReactor.RunFastQuery($"DELETE FROM rooms_rights WHERE room_id = {roomId}");
					queryReactor.RunFastQuery($"UPDATE users SET home_room = '0' WHERE home_room = {roomId}");
				}

				RoomData roomData2 =
					(from p in session.GetHabbo().UsersRooms where p.Id == roomId select p).SingleOrDefault();

				if (roomData2 != null)
					session.GetHabbo().UsersRooms.Remove(roomData2);
			}
		}
	}
}

