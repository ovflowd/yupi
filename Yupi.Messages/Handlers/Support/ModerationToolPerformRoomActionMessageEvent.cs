using System;
using Yupi.Model.Repository;
using Yupi.Model.Domain;
using Yupi.Controller;
using Yupi.Model;
using Yupi.Util;
using Yupi.Messages.Contracts;
using System.Linq;
using System.Collections;
using System.Collections.Generic;


namespace Yupi.Messages.Support
{
	public class ModerationToolPerformRoomActionMessageEvent : AbstractHandler
	{
		private IRepository<RoomData> RoomRepository;
		private RoomManager RoomManager;

		public ModerationToolPerformRoomActionMessageEvent ()
		{
			RoomRepository = DependencyFactory.Resolve<IRepository<RoomData>> ();
			RoomManager = DependencyFactory.Resolve<RoomManager> ();
		}

		public override void HandleMessage (Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			if (!session.Info.HasPermission ("fuse_mod"))
				return;

			int roomId = message.GetInteger ();

			// TODO Refactor (shoud be enum)
			bool lockRoom = message.GetIntegerAsBool ();
			bool inappropriateRoom = message.GetIntegerAsBool ();
			bool kickUsers = message.GetIntegerAsBool ();

			RoomData roomData = RoomRepository.FindBy (roomId);

			if (roomData == null) {
				return;
			}

			if (lockRoom) {
				roomData.State = RoomState.LOCKED;
			}

			Room room = null;

			if (inappropriateRoom || kickUsers) {
				room = RoomManager.LoadedRooms.FirstOrDefault (x => x.Data.Id == roomData.Id);
			}

			if (inappropriateRoom) {
				// TODO Translate
				roomData.Name = T._ ("Inappropriate for Hotel Management");
				roomData.Description = string.Empty;
				roomData.Tags.Clear ();

				if (room != null) {
					room.Each ((entitySession) => {
						entitySession.Router.GetComposer<RoomDataMessageComposer> ().Compose (entitySession, roomData, entitySession.Info, false, true);
					});
				}
			}

			if (kickUsers && room != null) {
				RoomManager.KickAll (room);
			}
		}
	}
}

