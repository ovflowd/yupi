using System;
using Yupi.Model.Domain;
using System.Collections.Generic;
using System.Linq;
using Yupi.Messages.Contracts;
using Yupi.Model.Repository;
using Yupi.Model;
using Yupi.Util;

namespace Yupi.Controller
{
	public class RoomManager
	{
		private List<Room> _loadedRooms;
		private IRepository<RoomData> RoomRepository;

		public IReadOnlyList<Room> LoadedRooms {
			get {
				return _loadedRooms.AsReadOnly ();
			}
		}

		public RoomManager ()
		{
			_loadedRooms = new List<Room> (); 
			RoomRepository = DependencyFactory.Resolve<IRepository<RoomData>> ();
		}

		public bool isLoaded(RoomData room) {
			return _loadedRooms.Any (x => x.Data == room);
		}

		public Room LoadOrGet(int roomId) {
			RoomData data = RoomRepository.FindBy (roomId);
			return LoadOrGet (data);
		}

		public Room LoadOrGet(RoomData data) {
			if (data == null) {
				return null;
			}

			Room room = GetIfLoaded (data);

			if (room == null) {
				room = new Room (data);
				_loadedRooms.Add (room);
			}

			return room;
		}

		public Room GetIfLoaded(RoomData room) {
			return _loadedRooms.FirstOrDefault (x => x.Data == room);
		}

		public int UsersNow(RoomData data) {
			Room room = GetIfLoaded (data);
			return room == null ? 0 : room.GetUserCount ();
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
			if (entity == null) {
				return;
			}

			Room room = entity.Room;

			room.Users.Remove (entity);

			if (entity.Type == EntityType.User) {
				UserEntity user = (UserEntity)entity;
				user.User.Router.GetComposer<OutOfRoomMessageComposer> ()
				.Compose (user.User, 2);

				user.User.IsRidingHorse = false;
			}
		}

		public IOrderedEnumerable<Room> GetActive() {
			return LoadedRooms
				.Where (x => x.GetUserCount () > 0)
				.OrderByDescending (x => x.GetUserCount ());
		}

		public IEnumerable<RoomData> GetEventRooms() {
			return RoomRepository
				.FilterBy (x => x.Event != null && !x.Event.HasExpired ())
				.OrderByDescending (x => UsersNow (x))
				.AsEnumerable();
		}

		public IList<RoomData> Search(string query) {
			bool containsOwner = false, containsGroup = false;

			// TODO Can't both be combined like a search on google: owner:Noob group:NoobGroup RandomName
			if (query.StartsWith ("owner:")) {
				query = query.Replace ("owner:", string.Empty);

				containsOwner = true;
			} else if (query.StartsWith ("group:")) {
				query = query.Replace ("group:", string.Empty);

				containsGroup = true;
			}

			query = query.Trim ();

			List<RoomData> rooms = new List<RoomData> ();

			if (containsOwner) {
				rooms = RoomRepository.FilterBy (x => x.Owner.UserName == query).ToList ();
			} else if (containsGroup) {
				rooms = RoomRepository.FilterBy (x => x.Group.Name == query).ToList ();
			} else {
				rooms.AddRange (LoadedRooms.Where (x => x.Data.Name.ContainsIgnoreCase (query)).Take (50).Select(x => x.Data));

				if (rooms.Count < 50) {
					// Use ToUpper() as that can be translated to SQL
					rooms.AddRange (RoomRepository.FilterBy (x => x.Name.ToUpper().Contains(query.ToUpper())).Take(50 - rooms.Count));
				}
			}

			return rooms;
		}
	}
}

