using System;
using Yupi.Protocol;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using Yupi.Model.Domain.Components;
using System.Threading;

namespace Yupi.Model.Domain
{
	[Ignore]
	public class Room : ISender
	{
		public IList<UserInfo> Queue { get; private set; }

		public RoomData Data { get; private set; }

		public HeightMap HeightMap { get; private set; }

		public IRouter Router { get; private set; }

		// TODO Implementation detail -> Private!
		public IList<RoomEntity> Users { get; private set; }

		// TODO What is this used for?
		public ISet<Group> GroupsInRoom { get; private set; }

		// TODO Can this be implemented better?
		private int entityIdCounter;

		private Timer Timer;

		/// <summary>
		/// The period between timer ticks in milliseconds
		/// </summary>
		private const int TICK_PERIOD = 10000;

		public Room (RoomData data)
		{
			if (data == null) {
				throw new ArgumentNullException ();
			}

			this.Data = data;
			this.HeightMap = new HeightMap (this.Data.Model.Heightmap);

			Users = new List<RoomEntity> ();
			GroupsInRoom = new HashSet<Group> ();

			if (Data.Group != null) {
				GroupsInRoom.Add (Data.Group);
			}

			entityIdCounter = 0;
			this.Timer = new Timer (OnTick, null, 0, TICK_PERIOD);
		}

		private void OnTick (object state)
		{
			List<RoomEntity> changes = Users.Where (x => x.NeedsUpdate).ToList ();
		}

		public bool CanVote (UserInfo user)
		{
			return !user.RatedRooms.Contains (this.Data) && this.Data.Owner != user;
		}

		public int GetUserCount ()
		{
			return Users.Where (x => x.Type == EntityType.User).Count ();
		}

		public IEnumerable<Habbo> GetSessions ()
		{
			// TODO Reimplement Users properly to prevent these queries!
			return Users.Where (x => x.Type == EntityType.User).Select (x => ((UserEntity)x).User);
		}

		public bool HasUsers ()
		{
			return Users.Any (x => x.Type == EntityType.User);
		}
			
		// TODO Consider using back references...
		public RoomEntity GetEntity (int id)
		{
			return Users.Single (entity => entity.Id == id);
		}

		public void AddUser (Habbo user)
		{
			user.RoomEntity = new UserEntity (user, this, ++entityIdCounter);
			user.RoomEntity.Position = Data.Model.Door;
			user.RoomEntity.SetRotation (Data.Model.DoorOrientation);
			Users.Add (user.RoomEntity);
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

