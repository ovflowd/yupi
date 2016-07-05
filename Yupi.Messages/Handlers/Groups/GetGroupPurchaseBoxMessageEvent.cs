using System;

using System.Collections.Generic;
using System.Linq;

namespace Yupi.Messages.Groups
{
	public class GetGroupPurchaseBoxMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Net.ISession<GameClient> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			HashSet<RoomData> rooms = new HashSet<RoomData>(session.GetHabbo().UsersRooms.Where(x => x.Group == null));

			router.GetComposer<GroupPurchasePageMessageComposer> ().Compose (session, rooms);
		}
	}
}

