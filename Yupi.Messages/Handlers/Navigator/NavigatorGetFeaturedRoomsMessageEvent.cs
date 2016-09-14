namespace Yupi.Messages.Navigator
{
    using System;

    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;

    public class NavigatorGetFeaturedRoomsMessageEvent : AbstractHandler
    {
        #region Fields

        private IRepository<RoomData> RoomRepository;

        #endregion Fields

        #region Constructors

        public NavigatorGetFeaturedRoomsMessageEvent()
        {
            RoomRepository = DependencyFactory.Resolve<IRepository<RoomData>>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            int roomId = request.GetInteger();

            RoomData roomData = RoomRepository.FindBy(roomId);

            if (roomData == null)
                return;

            router.GetComposer<OfficialRoomsMessageComposer>().Compose(session, roomData);
        }

        #endregion Methods
    }
}