namespace Yupi.Messages.Support
{
    using System;

    using Yupi.Controller;
    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;

    public class ModerationToolRoomToolMessageEvent : AbstractHandler
    {
        #region Fields

        private RoomManager RoomManager;
        private IRepository<RoomData> RoomRepository;

        #endregion Fields

        #region Constructors

        public ModerationToolRoomToolMessageEvent()
        {
            RoomRepository = DependencyFactory.Resolve<IRepository<RoomData>>();
            RoomManager = DependencyFactory.Resolve<RoomManager>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message,
            Yupi.Protocol.IRouter router)
        {
            if (!session.Info.HasPermission("fuse_mod"))
                return;

            int roomId = message.GetInteger();

            RoomData room = RoomRepository.FindBy(roomId);

            router.GetComposer<ModerationRoomToolMessageComposer>().Compose(session, room, RoomManager.isLoaded(room));
        }

        #endregion Methods
    }
}