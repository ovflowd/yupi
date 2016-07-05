using System;


namespace Yupi.Messages.Groups
{
	public class UpdateThreadMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Net.ISession<GameClient> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			uint groupId = request.GetUInt32();
			uint threadId = request.GetUInt32();
			bool pin = request.GetBool();
			bool Lock = request.GetBool();

			using (IQueryAdapter dbClient = Yupi.GetDatabaseManager().GetQueryReactor())
			{
				dbClient.SetQuery(
					$"SELECT * FROM groups_forums_posts WHERE group_id = '{groupId}' AND id = '{threadId}' LIMIT 1;");
				DataRow row = dbClient.GetRow();

				Group theGroup = Yupi.GetGame().GetGroupManager().GetGroup(groupId);

				if (row != null)
				{
					if ((uint) row["poster_id"] == Session.GetHabbo().Id ||
						theGroup.Admins.ContainsKey(Session.GetHabbo().Id))
					{
						dbClient.SetQuery(
							$"UPDATE groups_forums_posts SET pinned = @pin , locked = @lock WHERE id = {threadId};");
						dbClient.AddParameter("pin", pin ? "1" : "0");
						dbClient.AddParameter("lock", Lock ? "1" : "0");
						dbClient.RunQuery();
					}
				}

				GroupForumPost thread = new GroupForumPost(row);

				if (thread.Pinned != pin)
				{
					router.GetComposer<SuperNotificationMessageComposer>().Compose(session, string.Empty, string.Empty, string.Empty, string.Empty,
						pin ? "forums.thread.pinned" : "forums.thread.unpinned", 0);
				}

				if (thread.Locked != Lock)
				{
					router.GetComposer<SuperNotificationMessageComposer>().Compose(session, string.Empty, string.Empty, string.Empty, string.Empty,
						Lock ? "forums.thread.locked" : "forums.thread.unlocked", 0);
				}

				if (thread.ParentId != 0)
					return;

				router.GetComposer<GroupForumThreadUpdateMessageComposer>().Compose(session, groupId, thread, pin, Lock);
			}
		}
	}
}

