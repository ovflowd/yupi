using System;

using System.Data;

using Yupi.Messages.Notification;

namespace Yupi.Messages.Groups
{
	public class AlterForumThreadStateMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			uint groupId = request.GetUInt32();
			uint threadId = request.GetUInt32();
			int stateToSet = request.GetInteger();

			using (IQueryAdapter dbClient = Yupi.GetDatabaseManager().GetQueryReactor())
			{
				dbClient.SetQuery(
					$"SELECT * FROM groups_forums_posts WHERE group_id = '{groupId}' AND id = '{threadId}' LIMIT 1;");

				DataRow row = dbClient.GetRow();
				Group theGroup = Yupi.GetGame().GetGroupManager().GetGroup(groupId);

				if (row != null)
				{
					if ((uint) row["poster_id"] == session.GetHabbo().Id ||
						theGroup.Admins.ContainsKey(session.GetHabbo().Id))
					{
						dbClient.SetQuery($"UPDATE groups_forums_posts SET hidden = @hid WHERE id = {threadId};");
						dbClient.AddParameter("hid", stateToSet == 20 ? "1" : "0");
						dbClient.RunQuery();
					}
				}

				GroupForumPost thread = new GroupForumPost(row);

				router.GetComposer<SuperNotificationMessageComposer>().Compose(session, string.Empty, string.Empty, string.Empty, string.Empty,
					stateToSet == 20 ? "forums.thread.hidden" : "forums.thread.restored", 0);

				if (thread.ParentId != 0)
					return;

				router.GetComposer<GroupForumThreadUpdateMessageComposer>().Compose(session, groupId, thread, thread.Pinned, thread.Locked);
			}
		}
	}
}

