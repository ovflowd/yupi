using System;
using System.Collections.Generic;
using Yupi.Controller;
using Yupi.Model;
using Yupi.Model.Domain;
using System.Linq;

namespace Yupi.Messages.User
{
	public class FindMoreFriendsMessageEvent : AbstractHandler
	{
		private RoomManager RoomManager;
		private Random Random;

		public FindMoreFriendsMessageEvent ()
		{
			RoomManager = DependencyFactory.Resolve<RoomManager> ();
			Random = DependencyFactory.Resolve<Random> ();
		}

		public override void HandleMessage (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			IReadOnlyList<Room> rooms = RoomManager.LoadedRooms.Where(x => x.Users.Any()).ToList();

			router.GetComposer<FindMoreFriendsSuccessMessageComposer> ().Compose (session, rooms.Any());

			if (rooms.Any()) {
				int rand = Random.Next (rooms.Count - 1);

				router.GetComposer<RoomForwardMessageComposer> ().Compose (session, rooms[rand].Data.Id);
			}
		}
	}
}

