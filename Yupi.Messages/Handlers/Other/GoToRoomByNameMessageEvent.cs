namespace Yupi.Messages.Other
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Yupi.Controller;
    using Yupi.Messages.User;
    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Util;

    public class GoToRoomByNameMessageEvent : AbstractHandler
    {
        #region Fields

        private RoomManager RoomManager;

        #endregion Fields

        #region Constructors

        public GoToRoomByNameMessageEvent()
        {
            RoomManager = DependencyFactory.Resolve<RoomManager>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            string name = request.GetString();

            // TODO Refactor
            switch (name)
            {
                case "predefined_noob_lobby":
                {
                    /*
                    int roomId = Convert.ToUInt32(Yupi.GetDbConfig().DbData["noob.lobby.roomid"]);
                    router.GetComposer<RoomForwardMessageComposer> ().Compose (session, roomId);*/
                    throw new NotImplementedException();
                    break;
                }
                case "random_friending_room":
                {
                    Room room = RoomManager.GetActive().ToList().Random();
                    router.GetComposer<RoomForwardMessageComposer>().Compose(session, room.Data.Id);
                    break;
                }
            }
        }

        #endregion Methods
    }
}