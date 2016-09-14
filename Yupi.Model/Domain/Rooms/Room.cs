using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading;
using Yupi.Util.Pathfinding;

namespace Yupi.Model.Domain
{
    [Ignore]
    public class Room : IDisposable
    {
        [Ignore]
        public delegate void OnEntityCreate(RoomEntity entity);

        [Ignore]
        public delegate void OnHumanEntityCreateT(HumanEntity entity);

        [Ignore]
        public delegate void OnRoomTick(Room room, List<RoomEntity> changes);

        // TODO Experiment with the length of the period
        /// <summary>
        ///     The period between timer ticks in milliseconds
        /// </summary>
        private const int TICK_PERIOD = 500;

        // TODO Can this be implemented better?
        private int entityIdCounter;

        public OnEntityCreate OnEntityCreateCallback;
        public OnHumanEntityCreateT OnHumanEntityCreate;

        private readonly OnRoomTick OnTickCallback;

        private readonly Timer Timer;

        public Room(RoomData data, OnRoomTick onTickCallback)
        {
            Contract.Requires(onTickCallback != null);
            Contract.Requires(data != null);

            OnTickCallback = onTickCallback;
            Data = data;
            HeightMap = new HeightMap(Data.Model.Heightmap);
            Pathfinder = new Pathfinder(HeightMap.IsWalkable, HeightMap.GetNeighbours);

            Users = new List<RoomEntity>();
            GroupsInRoom = new HashSet<Group>();

            if (Data.Group != null) GroupsInRoom.Add(Data.Group);

            entityIdCounter = 0;
            Timer = new Timer(OnTick, null, 0, TICK_PERIOD);
        }

        public IList<UserInfo> Queue { get; private set; }

        public RoomData Data { get; }

        public HeightMap HeightMap { get; }

        // TODO Implementation detail -> Private!
        public IList<RoomEntity> Users { get; }

        // TODO What is this used for?
        public ISet<Group> GroupsInRoom { get; }
        public Pathfinder Pathfinder { get; private set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing) Timer.Dispose();
        }

        private void OnTick(object state)
        {
            var changes = new List<RoomEntity>(Users.Count);

            foreach (var entity in Users)
            {
                if (entity.HasSteps()) entity.NextStep();

                if (entity.NeedsUpdate) changes.Add(entity);
            }

            OnTickCallback(this, changes);

            foreach (var entity in changes) entity.UpdateComplete();
        }

        public bool CanVote(UserInfo user)
        {
            return !user.RatedRooms.Contains(Data) && (Data.Owner != user);
        }

        public int GetUserCount()
        {
            return Users.Where(x => x.Type == EntityType.User).Count();
        }

        private IEnumerable<Habbo> GetSessions()
        {
            // TODO Reimplement Users properly to prevent these queries!
            return Users.Where(x => x.Type == EntityType.User).Select(x => ((UserEntity) x).User);
        }

        public bool HasUsers()
        {
            return Users.Any(x => x.Type == EntityType.User);
        }

        // TODO Consider using back references...
        public RoomEntity GetEntity(int id)
        {
            return Users.SingleOrDefault(entity => entity.Id == id);
        }

        public RoomEntity GetEntity(string name)
        {
            return Users.SingleOrDefault(entity => entity.BaseInfo.Name == name);
        }

        public void AddUser(Habbo user)
        {
            user.RoomEntity = new UserEntity(user, this, ++entityIdCounter);
            user.RoomEntity.SetPosition(Data.Model.Door);
            user.RoomEntity.SetRotation(Data.Model.DoorOrientation);

            OnEntityCreateCallback(user.RoomEntity);
            OnHumanEntityCreate(user.RoomEntity);

            Users.Add(user.RoomEntity);
        }

        /// <summary>
        ///     Executes the callback for every REAL Session
        /// </summary>
        /// <remarks>
        ///     This function won't generate a callback for bots!
        /// </remarks>
        /// <param name="sendToUser">The callback (most likely used to broadcast a message)</param>
        public void EachUser(Action<Habbo> sendToUser)
        {
            foreach (var session in GetSessions()) sendToUser(session);
        }

        public void EachBot(Action<BotEntity> foreachBot)
        {
            foreach (var entity in Users.OfType<BotEntity>()) foreachBot(entity);
        }

        public void EachEntity(Action<RoomEntity> foreachEntity)
        {
            foreach (var entity in Users) foreachEntity(entity);
        }
    }
}