using System;

using System.Data;
using System.Collections.Generic;
using System.Linq;


namespace Yupi.Messages.Groups
{
	public class GetGroupForumThreadRootMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			uint groupId = request.GetUInt32();

			int startIndex = request.GetInteger();

			using (IQueryAdapter dbClient = Yupi.GetDatabaseManager().GetQueryReactor())
			{
				dbClient.SetQuery($"SELECT count(id) FROM groups_forums_posts WHERE group_id = '{groupId}' AND parent_id = 0");

				dbClient.GetInteger();

				dbClient.SetQuery($"SELECT * FROM groups_forums_posts WHERE group_id = '{groupId}' AND parent_id = 0 ORDER BY timestamp DESC, pinned DESC LIMIT {startIndex}, {TotalPerPage}");

				DataTable table = dbClient.GetTable();

				List<GroupForumPost> threads = (from DataRow row in table.Rows select new GroupForumPost(row)).ToList();

				router.GetComposer<GroupForumThreadRootMessageComposer> ().Compose (session, groupId, startIndex, threads);
			}
		}
	}
}

