using System;


using System.Data;

namespace Yupi.Messages.Groups
{
	public class PublishForumThreadMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			// TODO Flood protection?
			if (Yupi.GetUnixTimeStamp() - session.GetHabbo().LastSqlQuery < 20)
				return;

			uint groupId = request.GetUInt32();
			uint threadId = request.GetUInt32();
			string subject = request.GetString();
			string content = request.GetString();

			Group group = Yupi.GetGame().GetGroupManager().GetGroup(groupId);

			if (group == null || group.Forum.Id == 0)
				return;

			// TODO Millenium bug
			int timestamp = Yupi.GetUnixTimeStamp();

			using (IQueryAdapter dbClient = Yupi.GetDatabaseManager().GetQueryReactor())
			{
				if (threadId != 0)
				{
					dbClient.SetQuery("SELECT * FROM groups_forums_posts WHERE id = @threadId");
					dbClient.AddParameter("threadId", threadId);

					DataRow row = dbClient.GetRow();
					GroupForumPost post = new GroupForumPost(row);

					if (post.Locked || post.Hidden)
					{
						session.SendNotif(Yupi.GetLanguage().GetVar("forums_cancel"));
						return;
					}
				}

				session.GetHabbo().LastSqlQuery = Yupi.GetUnixTimeStamp();
				dbClient.SetQuery(
					"INSERT INTO groups_forums_posts (group_id, parent_id, timestamp, poster_id, poster_name, poster_look, subject, post_content) VALUES (@gid, @pard, @ts, @pid, @pnm, @plk, @subjc, @content)");
				dbClient.AddParameter("gid", groupId);
				dbClient.AddParameter("pard", threadId);
				dbClient.AddParameter("ts", timestamp);
				dbClient.AddParameter("pid", session.GetHabbo().Id);
				dbClient.AddParameter("pnm", session.GetHabbo().UserName);
				dbClient.AddParameter("plk", session.GetHabbo().Look);
				dbClient.AddParameter("subjc", subject);
				dbClient.AddParameter("content", content);

				threadId = (uint) dbClient.GetInteger();
			}

			group.Forum.ForumScore += 0.25;
			group.Forum.ForumLastPosterName = session.GetHabbo().UserName;
			group.Forum.ForumLastPosterId = session.GetHabbo().Id;
			group.Forum.ForumLastPosterTimestamp = (uint) timestamp;
			group.Forum.ForumMessagesCount++;
			group.UpdateForum();

			if (threadId == 0)
			{
				router.GetComposer<GroupForumNewThreadMessageComposer> ().Compose (session, groupId, threadId, session.GetHabbo ().Id, subject, content, timestamp);
			}
			else
			{
				router.GetComposer<GroupForumNewResponseMessageComposer> ().Compose (
					session, groupId, threadId, group.Forum.ForumMessagesCount, session.GetHabbo (), timestamp);
			}
		}
	}
}

