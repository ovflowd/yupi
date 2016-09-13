using System;
using Yupi.Protocol;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using Yupi.Model.Domain.Components;
using System.Threading;
using System.Diagnostics.Contracts;
using Yupi.Util;
using System.Numerics;

namespace Yupi.Model.Domain
{
	[Ignore]
	public class Room : IDisposable
	{
		public IList<UserInfo> Queue { get; private set; }

		public RoomData Data { get; private set; }

		public HeightMap HeightMap { get; private set; }

		// TODO Implementation detail -> Private!
		public IList<RoomEntity> Users { get; private set; }

		// TODO What is this used for?
		public ISet<Group> GroupsInRoom { get; private set; }

		// TODO Can this be implemented better?
		private int entityIdCounter;

		private Timer Timer;
		public AStar<Vector2> Pathfinder { get; private set; }

		// TODO Experiment with the length of the period
		/// <summary>
		/// The period between timer ticks in milliseconds
		/// </summary>
		private const int TICK_PERIOD = 500;

		[Ignore]
		public delegate void OnRoomTick (Room room, List<RoomEntity> changes);
		[Ignore]
		public delegate void OnEntityCreate(RoomEntity entity);

		[Ignore]
		public delegate void OnHumanEntityCreateT(HumanEntity entity);

		private OnRoomTick OnTickCallback;

		public OnEntityCreate OnEntityCreateCallback;
		public OnHumanEntityCreateT OnHumanEntityCreate;

		public Room (RoomData data, OnRoomTick onTickCallback)
		{
			Contract.Requires(onTickCallback != null);
			Contract.Requires(data != null);

			this.OnTickCallback = onTickCallback;
			this.Data = data;
			this.HeightMap = new HeightMap (this.Data.Model.Heightmap);
			this.Pathfinder = new AStar<Vector2> (HeightMap.IsWalkable, HeightMap.GetNeighbours, Vector2.Distance);

			Users = new List<RoomEntity> ();
			GroupsInRoom = new HashSet<Group> ();

			if (Data.Group != null) {
				GroupsInRoom.Add (Data.Group);
			}

			entityIdCounter = 0;
			this.Timer = new Timer (OnTick, null, 0, TICK_PERIOD);
		}

		public void Dispose ()
		{
			Dispose (true);
			GC.SuppressFinalize (this);
		}

		protected virtual void Dispose (bool disposing)
		{
			if (disposing) {
				Timer.Dispose ();
			}
		}

		private void OnTick (object state)
		{
			List<RoomEntity> changes = new List<RoomEntity> (this.Users.Count);

			foreach (RoomEntity entity in this.Users) {
				if (entity.HasSteps()) {
					entity.NextStep ();
				}

				if (entity.NeedsUpdate) {
					changes.Add (entity);
				}
			}

			this.OnTickCallback (this, changes);

			foreach (RoomEntity entity in changes) {
				entity.UpdateComplete ();
			}
		}

		public bool CanVote (UserInfo user)
		{
			return !user.RatedRooms.Contains (this.Data) && this.Data.Owner != user;
		}

		public int GetUserCount ()
		{
			return Users.Where (x => x.Type == EntityType.User).Count ();
		}

		private IEnumerable<Habbo> GetSessions ()
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
			return Users.SingleOrDefault (entity => entity.Id == id);
		}

		public RoomEntity GetEntity (string name)
		{
			return Users.SingleOrDefault (entity => entity.BaseInfo.Name == name);
		}

		public void AddUser (Habbo user)
		{
			user.RoomEntity = new UserEntity (user, this, ++entityIdCounter);
			user.RoomEntity.SetPosition (Data.Model.Door);
			user.RoomEntity.SetRotation (Data.Model.DoorOrientation);

			this.OnEntityCreateCallback (user.RoomEntity);
			this.OnHumanEntityCreate (user.RoomEntity);

			Users.Add (user.RoomEntity);
		}

		/// <summary>
		/// Executes the callback for every REAL Session
		/// </summary>
		/// <remarks>
		/// This function won't generate a callback for bots!
		/// </remarks>
		/// <param name="sendToUser">The callback (most likely used to broadcast a message)</param>
		public void EachUser (Action<Habbo> sendToUser)
		{
			foreach (Habbo session in GetSessions()) {
				sendToUser (session);
			}
		}

		public void EachBot(Action<BotEntity> foreachBot) {
			foreach (BotEntity entity in Users.OfType<BotEntity>()) {
				foreachBot (entity);
			}
		}

		public void EachEntity (Action<RoomEntity> foreachEntity)
		{
			foreach (RoomEntity entity in Users) {
				foreachEntity (entity);
			}
		}
	}
}