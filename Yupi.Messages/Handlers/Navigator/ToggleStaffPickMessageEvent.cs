using System;
using Yupi.Controller;
using Yupi.Messages.Rooms;
using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Navigator
{
    public class ToggleStaffPickMessageEvent : AbstractHandler
    {
        private readonly AchievementManager AchievementManager;
        private readonly RoomManager RoomManager;
        private readonly IRepository<RoomData> RoomRepository;

        public ToggleStaffPickMessageEvent()
        {
            RoomRepository = DependencyFactory.Resolve<IRepository<RoomData>>();
            AchievementManager = DependencyFactory.Resolve<AchievementManager>();
            RoomManager = DependencyFactory.Resolve<RoomManager>();
        }

        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            var roomId = request.GetInteger();

            request.GetBool(); // TODO Unused

            var roomData = RoomRepository.FindBy(roomId);

            if (roomData == null)
                return;

            AchievementManager.ProgressUserAchievement(roomData.Owner, "ACH_Spr", 1);

            // TODO Add room to Staff Pick category!
            throw new NotImplementedException();

            var room = RoomManager.GetIfLoaded(roomData);

            if (room != null)
                room.EachUser(
                    entitySession =>
                    {
                        entitySession.Router.GetComposer<RoomDataMessageComposer>()
                            .Compose(entitySession, roomData, entitySession.Info, true, true);
                    });
        }
    }
}