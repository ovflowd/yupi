using System;
using Yupi.Emulator.Game.Groups.Structs;
using Yupi.Emulator.Game.Rooms.User;
using Yupi.Messages.Rooms;
using Yupi.Emulator.Data.Base.Adapters.Interfaces;

namespace Yupi.Messages.Groups
{
	public class RemoveGroupAdminMessageEvent : AbstractHandler
	{
		// TODO Refactor
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			uint num = request.GetUInt32();
			uint num2 = request.GetUInt32();

			Group group = Yupi.GetGame().GetGroupManager().GetGroup(num);

			if (session.GetHabbo().Id != group.CreatorId || !group.Members.ContainsKey(num2) ||
				!group.Admins.ContainsKey(num2))
				return;

			group.Members[num2].Rank = 0;
			group.Admins.Remove(num2);

			Response.Init(PacketLibraryManager.OutgoingHandler("GroupMembersMessageComposer"));
			Yupi.GetGame().GetGroupManager().SerializeGroupMembers(Response, group, 0u, Session);
			SendResponse();

			Yupi.Messages.Rooms room = Yupi.GetGame().GetRoomManager().GetRoom(group.RoomId);
			RoomUser roomUserByHabbo = room?.GetRoomUserManager().GetRoomUserByHabbo(Yupi.GetHabboById(num2).UserName);

			if (roomUserByHabbo != null)
			{
				if (roomUserByHabbo.Statusses.ContainsKey("flatctrl 1"))
					roomUserByHabbo.RemoveStatus("flatctrl 1");

				router.GetComposer<RoomRightsLevelMessageComposer> ().Compose (session, 0);
				roomUserByHabbo.UpdateNeeded = true;
			}

			using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager ().GetQueryReactor ()) {
				// TODO Magic number !!! (rank)
				queryReactor.SetQuery ("UPDATE groups_members SET rank = 0 WHERE group_id = @group_id AND user_id = @user_id");
				queryReactor.AddParameter("group_id", num);
				queryReactor.AddParameter("user_id", num2);
				queryReactor.RunQuery ();
			}
		}
	}
}

