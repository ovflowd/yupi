using System;
using Yupi.Emulator.Game.Groups.Structs;
using Yupi.Emulator.Data.Base.Adapters.Interfaces;

namespace Yupi.Messages.Groups
{
	public class AcceptGroupRequestMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			uint groupId = request.GetUInt32();
			uint userId = request.GetUInt32();

			Group group = Yupi.GetGame().GetGroupManager().GetGroup(groupId);

			if (session.GetHabbo().Id != group.CreatorId && !group.Admins.ContainsKey(session.GetHabbo().Id) &&
				!group.Requests.ContainsKey(userId))
				return;

			if (group.Members.ContainsKey(userId))
			{
				group.Requests.Remove(userId);

				using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager ().GetQueryReactor ()) {
					queryReactor.SetQuery ("DELETE FROM group_requests WHERE group_id = @group_id AND user_id = @user_id");
					queryReactor.AddParameter("group_id", groupId);
					queryReactor.AddParameter("user_id", userId);
					queryReactor.RunQuery ();
				}
				return;
			}

			GroupMember memberGroup = group.Requests[userId];

			memberGroup.DateJoin = Yupi.GetUnixTimeStamp();
			group.Members.Add(userId, memberGroup);
			group.Requests.Remove(userId);
			group.Admins.Add(userId, group.Members[userId]);

			router.GetComposer<GroupDataMessageComposer> ().Compose (session, group, session.GetHabbo());
			router.GetComposer<GroupMembersMessageComposer> ().Compose (session, group, 0u, session);

			using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager ().GetQueryReactor ()) {
				queryReactor.SetQuery ("DELETE FROM group_requests WHERE group_id = @group_id AND user_id = @user_id");
				queryReactor.AddParameter("group_id", groupId);
				queryReactor.AddParameter("user_id", userId);
				queryReactor.RunQuery ();

				queryReactor.SetQuery ("INSERT INTO group_members (group_id, user_id, rank, date_join) VALUES (@group_id, @user_id, @rank, @timestamp)");
				queryReactor.AddParameter("group_id", groupId);
				queryReactor.AddParameter("user_id", userId);
				queryReactor.AddParameter("rank", 0);
				queryReactor.AddParameter("timestamp", Yupi.GetUnixTimeStamp());
				queryReactor.RunQuery ();
			}
		}
	}
}

