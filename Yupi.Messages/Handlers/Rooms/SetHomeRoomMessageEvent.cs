using System;
using Yupi.Controller;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Model;


namespace Yupi.Messages.Rooms
{
    public class SetHomeRoomMessageEvent : AbstractHandler
    {
        private Repository<RoomData> RoomRepository;
        private Repository<UserInfo> UserRepository;

        public SetHomeRoomMessageEvent()
        {
            RoomRepository = DependencyFactory.Resolve<Repository<RoomData>>();
            UserRepository = DependencyFactory.Resolve<Repository<UserInfo>>();
        }

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            int roomId = request.GetInteger();

            RoomData room = RoomRepository.FindBy(roomId);

            if (room != null)
            {
                session.Info.HomeRoom = room;
                UserRepository.Save(session.Info);
            }
        }
    }
}