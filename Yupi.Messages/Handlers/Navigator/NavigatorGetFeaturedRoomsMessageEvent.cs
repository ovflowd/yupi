using System;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Model;


namespace Yupi.Messages.Navigator
{
	public class NavigatorGetFeaturedRoomsMessageEvent : AbstractHandler
	{
		private IRepository<RoomData> RoomRepository;

		public NavigatorGetFeaturedRoomsMessageEvent ()
		{
			RoomRepository = DependencyFactory.Resolve<IRepository<RoomData>> ();
		}

		public override void HandleMessage ( Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			int roomId = request.GetInteger();

			RoomData roomData = RoomRepository.FindBy(roomId);

			if (roomData == null)
				return;

			router.GetComposer<OfficialRoomsMessageComposer> ().Compose (session, roomData);
		}
	}
}

