using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Rooms
{
    public class RoomGetInfoMessageEvent : AbstractHandler
    {
        private readonly IRepository<RoomData> RoomRepository;

        public RoomGetInfoMessageEvent()
        {
            RoomRepository = DependencyFactory.Resolve<IRepository<RoomData>>();
        }

        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            var roomId = request.GetInteger();

            // TODO num & num2 ???
            var num = request.GetInteger();
            var num2 = request.GetInteger();

            var room = RoomRepository.FindBy(roomId);

            if (room == null) return;

            var show = !((num == 0) && (num2 == 1));
            router.GetComposer<RoomDataMessageComposer>().Compose(session, room, session.Info, show, true);
            router.GetComposer<LoadRoomRightsListMessageComposer>().Compose(session, room);
        }
    }
}