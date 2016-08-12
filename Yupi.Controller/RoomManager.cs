using System;
using Yupi.Model.Domain;
using System.Collections.Generic;
using System.Linq;
using Yupi.Messages.Contracts;

namespace Yupi.Controller
{
	public class RoomManager
	{
		private List<Room> _loadedRooms;

		public IReadOnlyList<Room> LoadedRooms {
			get {
				return _loadedRooms.AsReadOnly ();
			}
		}

		public RoomManager ()
		{
			_loadedRooms = new List<Room> (); 
		}

		public bool isLoaded(RoomData room) {
			return _loadedRooms.Any (x => x.Data == room);
		}

		public void KickAll(Room room) {
			IEnumerable<RoomEntity> users = room.Users.Where (x => x.Type == EntityType.User);

			foreach (RoomEntity entity in users) {
				UserEntity user = (UserEntity)entity;

				if (user.UserInfo.HasPermission ("ignore_room_kick")) {
					continue;
				}

				RemoveUser (user);

				// TODO Stop effect
				// TODO Stop trade
			}
		}

		public void RemoveUser(RoomEntity entity) {
			Room room = entity.Room;

			room.Users.Remove (entity);

			if (entity.Type == EntityType.User) {
				UserEntity user = (UserEntity)entity;
				user.User.Session.Router.GetComposer<OutOfRoomMessageComposer> ()
				.Compose (user.User, 2);

				user.User.IsRidingHorse = false;
			}
		}
	}
}

