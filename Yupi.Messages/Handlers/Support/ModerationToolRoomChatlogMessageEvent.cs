using System;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Model;

namespace Yupi.Messages.Support
{
	public class ModerationToolRoomChatlogMessageEvent : AbstractHandler
	{
		private Repository<RoomData> RoomRepository;

		public ModerationToolRoomChatlogMessageEvent ()
		{
			RoomRepository = DependencyFactory.Resolve<Repository<RoomData>> ();
		}

		public override void HandleMessage (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			if (!session.UserData.Info.HasPermission ("fuse_chatlogs")) {
				return;
			}

			message.GetInteger (); // TODO Unused
			int roomId = message.GetInteger ();

			RoomData room = RoomRepository.FindBy (roomId);
			if (room != null) {
				router.GetComposer<ModerationToolRoomChatlogMessageComposer> ().Compose (session, room);
			}
		}
	}
}

