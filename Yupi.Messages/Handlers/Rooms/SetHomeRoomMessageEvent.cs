namespace Yupi.Messages.Rooms
{
    using System;

    using Yupi.Controller;
    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;

    public class SetHomeRoomMessageEvent : AbstractHandler
    {
        #region Fields

        private Repository<RoomData> RoomRepository;
        private Repository<UserInfo> UserRepository;

        #endregion Fields

        #region Constructors

        public SetHomeRoomMessageEvent()
        {
            RoomRepository = DependencyFactory.Resolve<Repository<RoomData>>();
            UserRepository = DependencyFactory.Resolve<Repository<UserInfo>>();
        }

        #endregion Constructors

        #region Methods

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

        #endregion Methods
    }
}