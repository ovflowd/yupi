using Yupi.Controller;
using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Support
{
    public class ModerationToolRoomToolMessageEvent : AbstractHandler
    {
        private readonly RoomManager RoomManager;
        private readonly IRepository<RoomData> RoomRepository;

        public ModerationToolRoomToolMessageEvent()
        {
            RoomRepository = DependencyFactory.Resolve<IRepository<RoomData>>();
            RoomManager = DependencyFactory.Resolve<RoomManager>();
        }

        public override void HandleMessage(Habbo session, ClientMessage message, IRouter router)
        {
            if (!session.Info.HasPermission("fuse_mod"))
                return;

            var roomId = message.GetInteger();

            var room = RoomRepository.FindBy(roomId);

            router.GetComposer<ModerationRoomToolMessageComposer>().Compose(session, room, RoomManager.isLoaded(room));
        }
    }
}