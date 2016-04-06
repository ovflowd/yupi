using System;
using System.Collections.Generic;

namespace Yupi.Messages.Catalog
{
	public class GetGroupFurnitureMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage message, Router router)
		{
			HashSet<GroupMember> userGroups = Yupi.GetGame().GetGroupManager().GetUserGroups(session.GetHabbo().Id);
		}
	}
}

