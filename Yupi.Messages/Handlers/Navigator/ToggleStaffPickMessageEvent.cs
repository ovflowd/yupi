namespace Yupi.Messages.Navigator
{
    using System;

    using Yupi.Controller;
    using Yupi.Messages.Rooms;
    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;

    public class ToggleStaffPickMessageEvent : AbstractHandler
    {
        #region Fields

        private AchievementManager AchievementManager;
        private RoomManager RoomManager;
        private IRepository<RoomData> RoomRepository;

        #endregion Fields

        #region Constructors

        public ToggleStaffPickMessageEvent()
        {
            RoomRepository = DependencyFactory.Resolve<IRepository<RoomData>>();
            AchievementManager = DependencyFactory.Resolve<AchievementManager>();
            RoomManager = DependencyFactory.Resolve<RoomManager>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            int roomId = request.GetInteger();

            request.GetBool(); // TODO Unused

            RoomData roomData = RoomRepository.FindBy(roomId);

            if (roomData == null)
                return;

            AchievementManager.ProgressUserAchievement(roomData.Owner, "ACH_Spr", 1);

            // TODO Add room to Staff Pick category!
            throw new NotImplementedException();

            Room room = RoomManager.GetIfLoaded(roomData);

            if (room != null)
            {
                room.EachUser(
                    (entitySession) =>
                    {
                        entitySession.Router.GetComposer<RoomDataMessageComposer>()
                            .Compose(entitySession, roomData, entitySession.Info, true, true);
                    });
            }
        }

        #endregion Methods
    }
}