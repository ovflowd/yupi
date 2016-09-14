using System;
using System.Linq;
using Yupi.Controller;
using Yupi.Messages.User;
using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;
using Yupi.Util;

namespace Yupi.Messages.Other
{
    public class GoToRoomByNameMessageEvent : AbstractHandler
    {
        private readonly RoomManager RoomManager;

        public GoToRoomByNameMessageEvent()
        {
            RoomManager = DependencyFactory.Resolve<RoomManager>();
        }

        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            var name = request.GetString();

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
                    var room = RoomManager.GetActive().ToList().Random();
                    router.GetComposer<RoomForwardMessageComposer>().Compose(session, room.Data.Id);
                    break;
                }
            }
        }
    }
}