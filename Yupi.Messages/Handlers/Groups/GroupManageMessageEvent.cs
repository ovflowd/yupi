using System;


namespace Yupi.Messages.Groups
{
	public class GroupManageMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			uint groupId = request.GetUInt32();
			Group group = Yupi.GetGame().GetGroupManager().GetGroup(groupId);

			if (group == null)
				return;

			// TODO Hardcoded value! (should use user rights instead of rank!)
			if (!group.Admins.ContainsKey(session.GetHabbo().Id) && group.CreatorId != session.GetHabbo().Id &&
				session.GetHabbo().Rank < 7)
				return;

			router.GetComposer<GroupDataEditMessageComposer> ().Compose (session, group);
		}
	}
}

