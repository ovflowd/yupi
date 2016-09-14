using System;

using System.Collections.Generic;
using System.Linq;
using Yupi.Model.Domain;

namespace Yupi.Messages.Groups
{
	public class GetGroupPurchaseBoxMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			List<RoomData> rooms = session.Info.UsersRooms.Where(x => x.Group == null).ToList();

			router.GetComposer<GroupPurchasePageMessageComposer> ().Compose (session, rooms);
		}
	}
}

