namespace Yupi.Messages.User
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Yupi.Controller;
    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Util;

    public class FindMoreFriendsMessageEvent : AbstractHandler
    {
        #region Fields

        private RoomManager RoomManager;

        #endregion Fields

        #region Constructors

        public FindMoreFriendsMessageEvent()
        {
            RoomManager = DependencyFactory.Resolve<RoomManager>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message,
            Yupi.Protocol.IRouter router)
        {
            List<Room> rooms = RoomManager.GetActive().ToList();

            router.GetComposer<FindMoreFriendsSuccessMessageComposer>().Compose(session, rooms.Any());

            Room room = rooms.Random();

            if (room != null)
            {
                router.GetComposer<RoomForwardMessageComposer>().Compose(session, room.Data.Id);
            }
        }

        #endregion Methods
    }
}