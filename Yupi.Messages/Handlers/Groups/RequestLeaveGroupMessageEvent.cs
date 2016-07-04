using System;
using Yupi.Emulator.Game.Groups.Structs;

namespace Yupi.Messages.Groups
{
	public class RequestLeaveGroupMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			uint groupId = request.GetUInt32();
			uint userId = request.GetUInt32();

			Group group = Yupi.GetGame().GetGroupManager().GetGroup(groupId);

			if (group == null || group.CreatorId == userId)
				return;

			if (userId == session.GetHabbo().Id || group.Admins.ContainsKey(session.GetHabbo().Id))
			{
				router.GetComposer<GroupAreYouSureMessageComposer> ().Compose (session, userId);
			}
		}
	}
}

