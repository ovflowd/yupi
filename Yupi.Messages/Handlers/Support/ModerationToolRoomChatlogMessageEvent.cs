using System;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Model;

namespace Yupi.Messages.Support
{
    public class ModerationToolRoomChatlogMessageEvent : AbstractHandler
    {
        private IRepository<RoomData> RoomRepository;

        public ModerationToolRoomChatlogMessageEvent()
        {
            RoomRepository = DependencyFactory.Resolve<IRepository<RoomData>>();
        }

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message,
            Yupi.Protocol.IRouter router)
        {
            if (!session.Info.HasPermission("fuse_chatlogs"))
            {
                return;
            }

            message.GetInteger(); // TODO Unused
            int roomId = message.GetInteger();

            RoomData room = RoomRepository.FindBy(roomId);
            if (room != null)
            {
                router.GetComposer<ModerationToolRoomChatlogMessageComposer>().Compose(session, room);
            }
        }
    }
}