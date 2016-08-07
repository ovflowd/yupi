using System;


using System.Data;
using System.Collections.Generic;

namespace Yupi.Messages.Groups
{
	public class ReadForumThreadMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			uint groupId = request.GetUInt32();
			uint threadId = request.GetUInt32();
			int startIndex = request.GetInteger();

			request.GetInteger(); // TODO Unused

			Group theGroup = Yupi.GetGame().GetGroupManager().GetGroup(groupId);

			if (theGroup == null || theGroup.Forum.Id == 0)
				return;

			using (IQueryAdapter dbClient = Yupi.GetDatabaseManager().GetQueryReactor())
			{
				dbClient.SetQuery(
					$"SELECT * FROM groups_forums_posts WHERE group_id = '{groupId}' AND parent_id = '{threadId}' OR id = '{threadId}' ORDER BY timestamp ASC;");

				DataTable table = dbClient.GetTable();

				if (table == null)
					return;

				int b = table.Rows.Count <= 20 ? table.Rows.Count : 20;

				List<GroupForumPost> posts = new List<GroupForumPost>();

				int i = 1;
				// TODO Refactor
				while (i <= b)
				{
					DataRow row = table.Rows[i - 1];

					if (row == null)
					{
						b--;
						continue;
					}

					GroupForumPost thread = new GroupForumPost(row);

					if (thread.ParentId == 0 && thread.Hidden)
						return;

					posts.Add(thread);

					i++;
				}

				router.GetComposer<GroupForumReadThreadMessageComposer> ().Compose (session, groupId, threadId, startIndex, b, posts);
			}
		}
	}
}

