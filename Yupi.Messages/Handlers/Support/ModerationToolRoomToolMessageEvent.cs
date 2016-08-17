using System;
using Yupi.Model;
using Yupi.Model.Repository;
using Yupi.Model.Domain;
using Yupi.Controller;


namespace Yupi.Messages.Support
{
	public class ModerationToolRoomToolMessageEvent : AbstractHandler
	{
		private IRepository<RoomData> RoomRepository;
		private RoomManager RoomManager;

		public ModerationToolRoomToolMessageEvent ()
		{
			RoomRepository = DependencyFactory.Resolve<IRepository<RoomData>> ();
			RoomManager = DependencyFactory.Resolve<RoomManager> ();
		}

		public override void HandleMessage ( Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			if (!session.Info.HasPermission("fuse_mod"))
				return;

			int roomId = message.GetInteger();

			RoomData room = RoomRepository.FindBy (roomId);

			router.GetComposer<ModerationRoomToolMessageComposer>().Compose(session, room, RoomManager.isLoaded(room));
		}
	}
}

