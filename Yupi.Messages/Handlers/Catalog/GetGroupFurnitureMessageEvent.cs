using System;
using System.Collections.Generic;

namespace Yupi.Messages.Catalog
{
	public class GetGroupFurnitureMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Net.ISession<IGameClient> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			HashSet<GroupMember> userGroups = Yupi.GetGame().GetGroupManager().GetUserGroups(session.GetHabbo().Id);
		}
	}
}

