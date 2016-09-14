using System.Linq;
using Yupi.Controller;
using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;
using Yupi.Util;

namespace Yupi.Messages.User
{
    public class FindMoreFriendsMessageEvent : AbstractHandler
    {
        private readonly RoomManager RoomManager;

        public FindMoreFriendsMessageEvent()
        {
            RoomManager = DependencyFactory.Resolve<RoomManager>();
        }

        public override void HandleMessage(Habbo session, ClientMessage message, IRouter router)
        {
            var rooms = RoomManager.GetActive().ToList();

            router.GetComposer<FindMoreFriendsSuccessMessageComposer>().Compose(session, rooms.Any());

            var room = rooms.Random();

            if (room != null) router.GetComposer<RoomForwardMessageComposer>().Compose(session, room.Data.Id);
        }
    }
}