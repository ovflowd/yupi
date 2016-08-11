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
		private Repository<RoomData> RoomRepository;
		private RoomManager RoomManager;

		public ModerationToolPerformRoomActionMessageEvent ()
		{
			RoomRepository = DependencyFactory.Resolve<Repository<RoomData>> ();
			RoomManager = DependencyFactory.Resolve<RoomManager> ();
		}

		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			if (!session.UserData.Info.HasPermission("fuse_mod"))
				return;

			int roomId = message.GetInteger();

			// TODO Refactor (shoud be enum)
			bool lockRoom = message.GetIntegerAsBool();
			bool inappropriateRoom = message.GetIntegerAsBool();
			bool kickUsers = message.GetIntegerAsBool();

			RoomData roomData = RoomRepository.FindBy (roomId);

			if (roomData == null) {
				return;
			}

			if (lockRoom)
			{
				roomData.State = RoomState.LOCKED;
			}

			Room room;

			if (inappropriateRoom || kickUsers) {
				room = RoomManager.LoadedRooms.FirstOrDefault (x => x.Data.Id == roomData.Id);
			}

			if (inappropriateRoom)
			{
				// TODO Translate
				roomData.Name = T._("Inappropriate for Hotel Management");
				roomData.Description = string.Empty;
				roomData.Tags.Clear();

				if (room != null) {
					room.Router.GetComposer<RoomDataMessageComposer> ().Compose (room, roomData, false, true);
				}
			}

			if (kickUsers) {
				IEnumerable<RoomEntity> users = room.Users.Where (x => x.Type == EntityType.User);

				foreach (RoomEntity entity in users) {
					UserEntity user = (UserEntity)entity;
					roomData.OnRoomKick ();
				}
			}
		}
	}
}

