using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Navigator
{
    public class NavigatorGetFeaturedRoomsMessageEvent : AbstractHandler
    {
        private readonly IRepository<RoomData> RoomRepository;

        public NavigatorGetFeaturedRoomsMessageEvent()
        {
            RoomRepository = DependencyFactory.Resolve<IRepository<RoomData>>();
        }

        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            var roomId = request.GetInteger();

            var roomData = RoomRepository.FindBy(roomId);

            if (roomData == null)
                return;

            router.GetComposer<OfficialRoomsMessageComposer>().Compose(session, roomData);
        }
    }
}