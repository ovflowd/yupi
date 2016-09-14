using System.Linq;
using Yupi.Controller;
using Yupi.Messages.Contracts;
using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;
using Yupi.Util;

namespace Yupi.Messages.Support
{
    public class ModerationToolPerformRoomActionMessageEvent : AbstractHandler
    {
        private readonly RoomManager RoomManager;
        private readonly IRepository<RoomData> RoomRepository;

        public ModerationToolPerformRoomActionMessageEvent()
        {
            RoomRepository = DependencyFactory.Resolve<IRepository<RoomData>>();
            RoomManager = DependencyFactory.Resolve<RoomManager>();
        }

        public override void HandleMessage(Habbo session, ClientMessage message, IRouter router)
        {
            if (!session.Info.HasPermission("fuse_mod"))
                return;

            var roomId = message.GetInteger();

            // TODO Refactor (shoud be enum)
            var lockRoom = message.GetIntegerAsBool();
            var inappropriateRoom = message.GetIntegerAsBool();
            var kickUsers = message.GetIntegerAsBool();

            var roomData = RoomRepository.FindBy(roomId);

            if (roomData == null) return;

            if (lockRoom) roomData.State = RoomState.Locked;

            Room room = null;

            if (inappropriateRoom || kickUsers)
                room = RoomManager.LoadedRooms.FirstOrDefault(x => x.Data.Id == roomData.Id);

            if (inappropriateRoom)
            {
                // TODO Translate
                roomData.Name = T._("Inappropriate for Hotel Management");
                roomData.Description = string.Empty;
                roomData.Tags.Clear();

                if (room != null)
                    room.EachUser(
                        entitySession =>
                        {
                            entitySession.Router.GetComposer<RoomDataMessageComposer>()
                                .Compose(entitySession, roomData, entitySession.Info, false, true);
                        });
            }

            if (kickUsers && (room != null)) RoomManager.KickAll(room);
        }
    }
}