using System;




namespace Yupi.Messages.Groups
{
	public class GroupUserJoinMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			uint groupId = request.GetUInt32();

			Group group = Yupi.GetGame().GetGroupManager().GetGroup(groupId);
			Habbo user = session.GetHabbo();

			if (!group.Members.ContainsKey(user.Id))
			{   // TODO Magic number !!!
				if (group.State == 0)
				{
					using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
					{
						queryReactor.SetQuery ("INSERT INTO group_members (group_id, user_id, rank, date_join) VALUES (@group_id, @user_id, @rank, @timestamp)");
						queryReactor.AddParameter("group_id", groupId);
						queryReactor.AddParameter("user_id", user.Id);
						queryReactor.AddParameter("rank", 0);
						queryReactor.AddParameter("timestamp", Yupi.GetUnixTimeStamp());
						queryReactor.RunQuery ();

						queryReactor.SetQuery ("UPDATE user_stats SET favourite_group = @group_id WHERE id = @user_id");
						queryReactor.AddParameter("group_id", groupId);
						queryReactor.AddParameter("user_id", user.Id);
						queryReactor.RunQuery ();
					}

					group.Members.Add(user.Id,
						new GroupMember(user.Id, user.UserName, user.Look, group.Id, 0, Yupi.GetUnixTimeStamp()));

					session.GetHabbo().UserGroups.Add(group.Members[user.Id]);
				}
				else
				{
					if (!group.Requests.ContainsKey(user.Id))
					{
						using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager ().GetQueryReactor ()) {
							queryReactor.SetQuery ("INSERT INTO groups_requests (user_id, group_id) VALUES (@user_id, @group_id)");
							queryReactor.AddParameter("group_id", groupId);
							queryReactor.AddParameter("user_id", user.Id);
							queryReactor.RunQuery ();
						}

						GroupMember groupRequest = new GroupMember(user.Id, user.UserName, user.Look, group.Id, 0,
							Yupi.GetUnixTimeStamp());

						group.Requests.Add(user.Id, groupRequest);
					}
				}

				router.GetComposer<GroupDataMessageComposer> ().Compose (session, group, session.GetHabbo());
			}
		}
	}
}

