using System;
using Yupi.Model;
using Yupi.Model.Repository;
using Yupi.Model.Domain;
using Yupi.Controller;


namespace Yupi.Messages.Support
{
	public class ModerationToolRoomToolMessageEvent : AbstractHandler
	{
		private Repository<RoomData> RoomRepository;
		private RoomManager RoomManager;

		public ModerationToolRoomToolMessageEvent ()
		{
			RoomRepository = DependencyFactory.Resolve<Repository<RoomData>> ();
			RoomManager = DependencyFactory.Resolve<RoomManager> ();
		}

		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			if (!session.UserData.Info.HasPermission("fuse_mod"))
				return;

			int roomId = message.GetInteger();

			RoomData room = RoomRepository.FindBy (roomId);

			router.GetComposer<ModerationRoomToolMessageComposer>().Compose(session, room, RoomManager.isLoaded(room));
		}
	}
}

