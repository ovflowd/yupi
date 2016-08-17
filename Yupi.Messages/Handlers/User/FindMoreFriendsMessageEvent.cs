using System;
using System.Collections.Generic;
using Yupi.Controller;
using Yupi.Model;
using Yupi.Model.Domain;
using System.Linq;
using Yupi.Util;

namespace Yupi.Messages.User
{
	public class FindMoreFriendsMessageEvent : AbstractHandler
	{
		private RoomManager RoomManager;

		public FindMoreFriendsMessageEvent ()
		{
			RoomManager = DependencyFactory.Resolve<RoomManager> ();
		}

		public override void HandleMessage (Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			List<Room> rooms = RoomManager.GetActive().ToList();

			router.GetComposer<FindMoreFriendsSuccessMessageComposer> ().Compose (session, rooms.Any());

			Room room = rooms.Random ();

			if (room != null) {
				router.GetComposer<RoomForwardMessageComposer> ().Compose (session, room.Data.Id);
			}
		}
	}
}

