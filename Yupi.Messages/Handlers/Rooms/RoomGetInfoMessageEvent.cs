namespace Yupi.Messages.Rooms
{
    using System;
    using System.Data;

    using Yupi.Controller;
    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;

    public class RoomGetInfoMessageEvent : AbstractHandler
    {
        #region Fields

        private IRepository<RoomData> RoomRepository;

        #endregion Fields

        #region Constructors

        public RoomGetInfoMessageEvent()
        {
            RoomRepository = DependencyFactory.Resolve<IRepository<RoomData>>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            int roomId = request.GetInteger();

            // TODO num & num2 ???
            int num = request.GetInteger();
            int num2 = request.GetInteger();

            RoomData room = RoomRepository.FindBy(roomId);

            if (room == null)
            {
                return;
            }

            bool show = !(num == 0 && num2 == 1);
            router.GetComposer<RoomDataMessageComposer>().Compose(session, room, session.Info, show, true);
            router.GetComposer<LoadRoomRightsListMessageComposer>().Compose(session, room);
        }

        #endregion Methods
    }
}