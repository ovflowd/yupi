namespace Yupi.Messages.Support
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using Yupi.Controller;
    using Yupi.Messages.Contracts;
    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;
    using Yupi.Util;

    public class ModerationToolPerformRoomActionMessageEvent : AbstractHandler
    {
        #region Fields

        private RoomManager RoomManager;
        private IRepository<RoomData> RoomRepository;

        #endregion Fields

        #region Constructors

        public ModerationToolPerformRoomActionMessageEvent()
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

            // TODO Refactor (shoud be enum)
            bool lockRoom = message.GetIntegerAsBool();
            bool inappropriateRoom = message.GetIntegerAsBool();
            bool kickUsers = message.GetIntegerAsBool();

            RoomData roomData = RoomRepository.FindBy(roomId);

            if (roomData == null)
            {
                return;
            }

            if (lockRoom)
            {
                roomData.State = RoomState.Locked;
            }

            Room room = null;

            if (inappropriateRoom || kickUsers)
            {
                room = RoomManager.LoadedRooms.FirstOrDefault(x => x.Data.Id == roomData.Id);
            }

            if (inappropriateRoom)
            {
                // TODO Translate
                roomData.Name = T._("Inappropriate for Hotel Management");
                roomData.Description = string.Empty;
                roomData.Tags.Clear();

                if (room != null)
                {
                    room.EachUser(
                        (entitySession) =>
                        {
                            entitySession.Router.GetComposer<RoomDataMessageComposer>()
                                .Compose(entitySession, roomData, entitySession.Info, false, true);
                        });
                }
            }

            if (kickUsers && room != null)
            {
                RoomManager.KickAll(room);
            }
        }

        #endregion Methods
    }
}