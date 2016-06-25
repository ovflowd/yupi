using System;
using Yupi.Emulator.Game.Groups.Structs;
using Yupi.Emulator.Data.Base.Adapters.Interfaces;

namespace Yupi.Messages.Groups
{
	public class UpdateForumSettingsMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			uint guild = request.GetUInt32();
			uint whoCanRead = request.GetUInt32();
			uint whoCanPost = request.GetUInt32();
			uint whoCanThread = request.GetUInt32();
			uint whoCanMod = request.GetUInt32();

			Group group = Yupi.GetGame().GetGroupManager().GetGroup(guild);

			if (group == null)
				return;

			// TODO Check rights?!
			group.Forum.WhoCanRead = whoCanRead;
			group.Forum.WhoCanPost = whoCanPost;
			group.Forum.WhoCanThread = whoCanThread;
			group.Forum.WhoCanMod = whoCanMod;

			using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
			{
				queryReactor.SetQuery(
					"UPDATE groups_forums_data SET who_can_read = @who_can_read, who_can_post = @who_can_post, who_can_thread = @who_can_thread, who_can_mod = @who_can_mod WHERE group_id = @group_id");
				queryReactor.AddParameter("group_id", group.Id);
				queryReactor.AddParameter("who_can_read", whoCanRead);
				queryReactor.AddParameter("who_can_post", whoCanPost);
				queryReactor.AddParameter("who_can_thread", whoCanThread);
				queryReactor.AddParameter("who_can_mod", whoCanMod);
				queryReactor.RunQuery();
			}

			session.SendMessage(group.ForumDataMessage(session.GetHabbo().Id));
		}
	}
}

