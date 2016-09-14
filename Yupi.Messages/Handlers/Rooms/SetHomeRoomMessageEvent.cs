using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Rooms
{
    public class SetHomeRoomMessageEvent : AbstractHandler
    {
        private readonly Repository<RoomData> RoomRepository;
        private readonly Repository<UserInfo> UserRepository;

        public SetHomeRoomMessageEvent()
        {
            RoomRepository = DependencyFactory.Resolve<Repository<RoomData>>();
            UserRepository = DependencyFactory.Resolve<Repository<UserInfo>>();
        }

        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            var roomId = request.GetInteger();

            var room = RoomRepository.FindBy(roomId);

            if (room != null)
            {
                session.Info.HomeRoom = room;
                UserRepository.Save(session.Info);
            }
        }
    }
}