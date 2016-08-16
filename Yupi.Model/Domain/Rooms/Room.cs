using System;
using Yupi.Protocol;
using System.Collections.Generic;
using System.Linq;

namespace Yupi.Model.Domain
{
	[Ignore]
	public class Room : ISender
	{
		public IList<UserInfo> Queue { get; private set; } 

		public RoomData Data { get; private set; }

		public IRouter Router { get; private set; }

		// TODO Implementation detail -> Private!
		public IList<RoomEntity> Users { get; private set; }

		// TODO What is this used for?
		public ISet<Group> GroupsInRoom { get; private set; }

		public Room (RoomData data)
		{
			if (data == null) {
				throw new ArgumentNullException ();
			}

			this.Data = data;
			Users = new List<RoomEntity> ();
			GroupsInRoom = new HashSet<Group> ();
			GroupsInRoom.Add (Data.Group);
		}

		public bool CanVote(UserInfo user) {
			return !user.RatedRooms.Contains (this.Data) && this.Data.Owner != user;
		}

		public int GetUserCount() {
			return Users.Where (x => x.Type == EntityType.User).Count ();
		}

		public IEnumerable<Habbo> GetSessions() {
			// TODO Reimplement Users properly to prevent these queries!
			return Users.Where (x => x.Type == EntityType.User).Select (x => ((UserEntity)x).User);
		}

		public bool HasUsers() {
			return Users.Any (x => x.Type == EntityType.User);
		}

		public bool HasOwnerRights(UserInfo user) {
			return (Data.Owner == user || user.HasPermission ("fuse_any_room_controller"));
		}

		public bool HasRights(UserInfo user) {
			return (Data.Owner == user || user.HasPermission ("fuse_any_rooms_rights") || Data.Rights.Contains(user));
		}

		// TODO Consider using back references...
		public RoomEntity GetEntity(int id) {
			return Users.Single (entity => entity.Id == id);
		}

		public void Send (Yupi.Protocol.Buffers.ServerMessage message)
		{
			foreach (RoomEntity entity in Users) {
				if (entity is UserEntity) {
					entity.Send (message);
				}
			}
		}
	}
}

