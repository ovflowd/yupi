using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Support
{
    public class ModerationToolRoomChatlogMessageEvent : AbstractHandler
    {
        private readonly IRepository<RoomData> RoomRepository;

        public ModerationToolRoomChatlogMessageEvent()
        {
            RoomRepository = DependencyFactory.Resolve<IRepository<RoomData>>();
        }

        public override void HandleMessage(Habbo session, ClientMessage message, IRouter router)
        {
            if (!session.Info.HasPermission("fuse_chatlogs")) return;

            message.GetInteger(); // TODO Unused
            var roomId = message.GetInteger();

            var room = RoomRepository.FindBy(roomId);
            if (room != null) router.GetComposer<ModerationToolRoomChatlogMessageComposer>().Compose(session, room);
        }
    }
}