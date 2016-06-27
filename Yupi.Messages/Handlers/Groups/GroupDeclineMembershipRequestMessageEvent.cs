using System;
using Yupi.Emulator.Game.Groups.Structs;
using Yupi.Emulator.Game.Rooms.User;
using Yupi.Emulator.Data.Base.Adapters.Interfaces;

namespace Yupi.Messages.Groups
{
	public class GroupDeclineMembershipRequestMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			uint groupId = request.GetUInt32();
			uint userId = request.GetUInt32();

			Group group = Yupi.GetGame().GetGroupManager().GetGroup(groupId);

			if (session.GetHabbo().Id != group.CreatorId && !group.Admins.ContainsKey(session.GetHabbo().Id) &&
				!group.Requests.ContainsKey(userId))
				return;

			group.Requests.Remove(userId);

			Response.Init(PacketLibraryManager.OutgoingHandler("GroupMembersMessageComposer"));
			Yupi.GetGame().GetGroupManager().SerializeGroupMembers(Response, group, 2u, Session);
			SendResponse();

			Yupi.Messages.Rooms room = Yupi.GetGame().GetRoomManager().GetRoom(group.RoomId);

			RoomUser roomUserByHabbo = room?.GetRoomUserManager().GetRoomUserByHabbo(Yupi.GetHabboById(userId).UserName);

			if (roomUserByHabbo != null)
			{
				if (roomUserByHabbo.Statusses.ContainsKey("flatctrl 1"))
					roomUserByHabbo.RemoveStatus("flatctrl 1");

				roomUserByHabbo.UpdateNeeded = true;
			}

			Yupi.GetGame().GetGroupManager().SerializeGroupInfo(group, Response, session);
		
			using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager ().GetQueryReactor ()) {
				queryReactor.SetQuery ("DELETE FROM group_requests WHERE group_id = @group_id AND user_id = @user_id");
				queryReactor.AddParameter("group_id", groupId);
				queryReactor.AddParameter("user_id", userId);
				queryReactor.RunQuery ();
			}
		}
	}
}

