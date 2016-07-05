using System;



using Yupi.Messages.Rooms;

namespace Yupi.Messages.Groups
{
	public class ConfirmLeaveGroupMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			uint groupId = request.GetUInt32();

			uint userId = request.GetUInt32();

			Group group = Yupi.GetGame().GetGroupManager().GetGroup(groupId);

			if (group == null)
				return;

			if (group.CreatorId == userId)
			{
				session.SendNotif(Yupi.GetLanguage().GetVar("user_room_video_true"));
				return;
			}

			// TODO Refactor
			if (userId == session.GetHabbo().Id || group.Admins.ContainsKey(session.GetHabbo().Id))
			{
				GroupMember memberShip;

				int type = 3;

				if (!group.Members.ContainsKey(userId) && !group.Admins.ContainsKey(userId))
					return;

				if (group.Members.ContainsKey(userId))
				{
					memberShip = group.Members[userId];

					type = 3;

					session.GetHabbo().UserGroups.Remove(memberShip);
					group.Members.Remove(userId);
				}

				if (group.Admins.ContainsKey(userId))
				{
					memberShip = group.Admins[userId];

					type = 1;

					session.GetHabbo().UserGroups.Remove(memberShip);
					group.Admins.Remove(userId);
				}   

				using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager ().GetQueryReactor ()) {
					queryReactor.SetQuery ("DELETE FROM groups_members WHERE user_id = @user_id AND group_id = @group_id");
					queryReactor.AddParameter ("user_id", userId);
					queryReactor.AddParameter ("group_id", groupId);
					queryReactor.RunQuery ();
				}

				Habbo byeUser = Yupi.GetHabboById(userId);

				if (byeUser != null)
				{
					router.GetComposer<GroupConfirmLeaveMessageComposer> ().Compose (session, byeUser, group, type);

					if (byeUser.FavouriteGroup == groupId) {
						byeUser.FavouriteGroup = 0;

						using (IQueryAdapter queryreactor2 = Yupi.GetDatabaseManager().GetQueryReactor())
							queryreactor2.RunFastQuery("UPDATE users_stats SET favourite_group = 0 WHERE id = " + userId + " LIMIT 1");

						Yupi.Messages.Rooms room = session.GetHabbo().CurrentRoom;

						if (room != null) {
							router.GetComposer<FavouriteGroupMessageComposer> ().Compose (room, byeUser.Id);
							router.GetComposer<ChangeFavouriteGroupMessageComposer> ().Compose (room, null, 0);
						} else {
							router.GetComposer<FavouriteGroupMessageComposer> ().Compose (session, byeUser.Id);
						}
					}
				}
					
				if (group.AdminOnlyDeco == 0u)
				{
					if (Yupi.GetGame().GetRoomManager().GetRoom(group.RoomId).GetRoomUserManager().GetRoomUserByHabbo(byeUser?.UserName) != null)
					{
						router.GetComposer<RoomRightsLevelMessageComposer> ().Compose (session, 0);
					}
				}
					
				router.GetComposer<GroupDataMessageComposer> ().Compose (session, group, session.GetHabbo());
				router.GetComposer<GroupMembersMessageComposer> ().Compose (session, group);
				router.GetComposer<GrouprequestReloadMessageComposer> ().Compose (session, groupId);
			}
		}
	}
}