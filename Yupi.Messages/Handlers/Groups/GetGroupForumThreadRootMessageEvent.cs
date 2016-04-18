using System;
using Yupi.Emulator.Data.Base.Adapters.Interfaces;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using Yupi.Emulator.Game.Groups.Structs;

namespace Yupi.Messages.Groups
{
	public class GetGroupForumThreadRootMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
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

