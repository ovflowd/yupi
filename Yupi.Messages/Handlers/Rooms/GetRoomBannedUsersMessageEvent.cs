using System;
using System.Collections.Generic;
using Yupi.Model.Repository;
using Yupi.Model.Domain;
using Yupi.Model;

namespace Yupi.Messages.Rooms
{
    public class GetRoomBannedUsersMessageEvent : AbstractHandler
    {
        private Repository<RoomData> RoomRepository;

        public GetRoomBannedUsersMessageEvent()
        {
            RoomRepository = DependencyFactory.Resolve<Repository<RoomData>>();
        }

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            int roomId = request.GetInteger();

            RoomData room = RoomRepository.FindBy(roomId);

            if (room != null && room.HasOwnerRights(session.Info))
            {
                router.GetComposer<RoomBannedListMessageComposer>().Compose(session, room);
            }
        }
    }
}