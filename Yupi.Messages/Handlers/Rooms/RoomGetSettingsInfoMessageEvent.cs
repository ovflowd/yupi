namespace Yupi.Messages.Rooms
{
    using System;

    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;

    public class RoomGetSettingsInfoMessageEvent : AbstractHandler
    {
        #region Fields

        private Repository<RoomData> RoomRepository;

        #endregion Fields

        #region Constructors

        public RoomGetSettingsInfoMessageEvent()
        {
            RoomRepository = DependencyFactory.Resolve<Repository<RoomData>>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            int roomId = request.GetInteger();

            RoomData room = RoomRepository.FindBy(roomId);

            if (room != null && room.HasOwnerRights(session.Info))
            {
                router.GetComposer<RoomSettingsDataMessageComposer>().Compose(session, room);
            }
        }

        #endregion Methods
    }
}