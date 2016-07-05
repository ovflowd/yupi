using System;


namespace Yupi.Messages.Groups
{
	public class GetGroupMembersMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Net.ISession<GameClient> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			uint groupId = request.GetUInt32();
			int page = request.GetInteger();
			string searchVal = request.GetString();
			uint reqType = request.GetUInt32();

			Group group = Yupi.GetGame().GetGroupManager().GetGroup(groupId);

			router.GetComposer<GroupMembersMessageComposer> ().Compose (session, group, reqType, session, searchVal, page);
		}
	}
}

