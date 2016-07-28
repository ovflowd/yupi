using System;
using System.Collections.Generic;

namespace Yupi.Messages.Catalog
{
	public class GetGroupFurnitureMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			HashSet<GroupMember> userGroups = Yupi.GetGame().GetGroupManager().GetUserGroups(session.GetHabbo().Id);
		}
	}
}

