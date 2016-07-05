using System;


namespace Yupi.Messages.Groups
{
	public class GetGroupForumDataMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			uint groupId = request.GetUInt32();

			Group group = Yupi.GetGame().GetGroupManager().GetGroup(groupId);

			if (group != null && group.Forum.Id != 0) {
				router.GetComposer<GroupForumDataMessageComposer> ().Compose (session, group, session.GetHabbo ().Id);
			}
		}
	}
}

