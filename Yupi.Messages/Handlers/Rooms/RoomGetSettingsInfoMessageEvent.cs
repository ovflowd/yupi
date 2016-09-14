using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Rooms
{
    public class RoomGetSettingsInfoMessageEvent : AbstractHandler
    {
        private readonly Repository<RoomData> RoomRepository;

        public RoomGetSettingsInfoMessageEvent()
        {
            RoomRepository = DependencyFactory.Resolve<Repository<RoomData>>();
        }

        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            var roomId = request.GetInteger();

            var room = RoomRepository.FindBy(roomId);

            if ((room != null) && room.HasOwnerRights(session.Info))
                router.GetComposer<RoomSettingsDataMessageComposer>().Compose(session, room);
        }
    }
}