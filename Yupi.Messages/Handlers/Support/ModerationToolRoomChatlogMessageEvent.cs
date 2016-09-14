namespace Yupi.Messages.Support
{
    using System;

    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;

    public class ModerationToolRoomChatlogMessageEvent : AbstractHandler
    {
        #region Fields

        private IRepository<RoomData> RoomRepository;

        #endregion Fields

        #region Constructors

        public ModerationToolRoomChatlogMessageEvent()
        {
            RoomRepository = DependencyFactory.Resolve<IRepository<RoomData>>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message,
            Yupi.Protocol.IRouter router)
        {
            if (!session.Info.HasPermission("fuse_chatlogs"))
            {
                return;
            }

            message.GetInteger(); // TODO Unused
            int roomId = message.GetInteger();

            RoomData room = RoomRepository.FindBy(roomId);
            if (room != null)
            {
                router.GetComposer<ModerationToolRoomChatlogMessageComposer>().Compose(session, room);
            }
        }

        #endregion Methods
    }
}