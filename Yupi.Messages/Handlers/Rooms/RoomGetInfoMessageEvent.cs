using System;

using System.Data;
using Yupi.Controller;
using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Model.Repository;


namespace Yupi.Messages.Rooms
{
	public class RoomGetInfoMessageEvent : AbstractHandler
	{
		private IRepository<RoomData> RoomRepository;

		public RoomGetInfoMessageEvent ()
		{
			RoomRepository = DependencyFactory.Resolve<IRepository<RoomData>> ();
		}

		public override void HandleMessage ( Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			int roomId = request.GetInteger();

			// TODO num & num2 ???
			int num = request.GetInteger();
			int num2 = request.GetInteger();

			RoomData room = RoomRepository.FindBy(roomId);

			if (room == null) {
				return;
			}

			bool show = !(num == 0 && num2 == 1);
			router.GetComposer<RoomDataMessageComposer> ().Compose (session, room, session.Info, show, true);
			router.GetComposer<LoadRoomRightsListMessageComposer> ().Compose (session, room);
		}
	}
}

