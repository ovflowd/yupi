using System;


namespace Yupi.Messages.Groups
{
	public class GetGroupForumDataMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Net.ISession<GameClient> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			uint groupId = request.GetUInt32();

			Group group = Yupi.GetGame().GetGroupManager().GetGroup(groupId);

			if (group != null && group.Forum.Id != 0) {
				router.GetComposer<GroupForumDataMessageComposer> ().Compose (session, group, session.GetHabbo ().Id);
			}
		}
	}
}

