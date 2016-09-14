using System;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Model;


namespace Yupi.Messages.Rooms
{
	public class RoomGetSettingsInfoMessageEvent : AbstractHandler
	{
		private Repository<RoomData> RoomRepository;

		public RoomGetSettingsInfoMessageEvent ()
		{
			RoomRepository = DependencyFactory.Resolve<Repository<RoomData>> ();
		}

		public override void HandleMessage ( Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			int roomId = request.GetInteger();

			RoomData room = RoomRepository.FindBy (roomId);

			if (room != null && room.HasOwnerRights(session.Info)) {
				router.GetComposer<RoomSettingsDataMessageComposer> ().Compose (session, room);
			}
		}
	}
}

