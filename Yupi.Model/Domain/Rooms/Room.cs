// ---------------------------------------------------------------------------------
// <copyright file="Room.cs" company="https://github.com/sant0ro/Yupi">
//   Copyright (c) 2016 Claudio Santoro, TheDoctor
// </copyright>
// <license>
//   Permission is hereby granted, free of charge, to any person obtaining a copy
//   of this software and associated documentation files (the "Software"), to deal
//   in the Software without restriction, including without limitation the rights
//   to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//   copies of the Software, and to permit persons to whom the Software is
//   furnished to do so, subject to the following conditions:
//
//   The above copyright notice and this permission notice shall be included in
//   all copies or substantial portions of the Software.
//
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//   IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//   FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//   AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//   LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//   OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//   THE SOFTWARE.
// </license>
// ---------------------------------------------------------------------------------
namespace Yupi.Model.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using System.Numerics;
    using System.Threading;

    using Yupi.Model.Domain.Components;
    using Yupi.Protocol;
    using Yupi.Util.Pathfinding;

    [Ignore]
    public class Room : IDisposable
    {
        #region Fields

        public OnEntityCreate OnEntityCreateCallback;
        public OnHumanEntityCreateT OnHumanEntityCreate;

        // TODO Experiment with the length of the period
        /// <summary>
        /// The period between timer ticks in milliseconds
        /// </summary>
        private const int TICK_PERIOD = 500;

        // TODO Can this be implemented better?
        private int entityIdCounter;
        private OnRoomTick OnTickCallback;
        private Timer Timer;

        #endregion Fields

        #region Delegates

        [Ignore]
        public delegate void OnEntityCreate(RoomEntity entity);

        [Ignore]
        public delegate void OnHumanEntityCreateT(HumanEntity entity);

        [Ignore]
        public delegate void OnRoomTick(Room room, List<RoomEntity> changes);

        #endregion Delegates

        #region Properties

        public RoomData Data
        {
            get; private set;
        }

        // TODO What is this used for?
        public ISet<Group> GroupsInRoom
        {
            get; private set;
        }

        public HeightMap HeightMap
        {
            get; private set;
        }

        public Pathfinder Pathfinder
        {
            get; private set;
        }

        public IList<UserInfo> Queue
        {
            get; private set;
        }

        // TODO Implementation detail -> Private!
        public IList<RoomEntity> Users
        {
            get; private set;
        }

        #endregion Properties

        #region Constructors

        public Room(RoomData data, OnRoomTick onTickCallback)
        {
            Contract.Requires(onTickCallback != null);
            Contract.Requires(data != null);

            this.OnTickCallback = onTickCallback;
            this.Data = data;
            this.HeightMap = new HeightMap(this.Data.Model.Heightmap);
            this.Pathfinder = new Pathfinder(HeightMap.IsWalkable, HeightMap.GetNeighbours);

            Users = new List<RoomEntity>();
            GroupsInRoom = new HashSet<Group>();

            if (Data.Group != null)
            {
                GroupsInRoom.Add(Data.Group);
            }

            entityIdCounter = 0;
            this.Timer = new Timer(OnTick, null, 0, TICK_PERIOD);
        }

        #endregion Constructors

        #region Methods

        public void AddUser(Habbo user)
        {
            user.RoomEntity = new UserEntity(user, this, ++entityIdCounter);
            user.RoomEntity.SetPosition(Data.Model.Door);
            user.RoomEntity.SetRotation(Data.Model.DoorOrientation);

            this.OnEntityCreateCallback(user.RoomEntity);
            this.OnHumanEntityCreate(user.RoomEntity);

            Users.Add(user.RoomEntity);
        }

        public bool CanVote(UserInfo user)
        {
            return !user.RatedRooms.Contains(this.Data) && this.Data.Owner != user;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void EachBot(Action<BotEntity> foreachBot)
        {
            foreach (BotEntity entity in Users.OfType<BotEntity>())
            {
                foreachBot(entity);
            }
        }

        public void EachEntity(Action<RoomEntity> foreachEntity)
        {
            foreach (RoomEntity entity in Users)
            {
                foreachEntity(entity);
            }
        }

        /// <summary>
        /// Executes the callback for every REAL Session
        /// </summary>
        /// <remarks>
        /// This function won't generate a callback for bots!
        /// </remarks>
        /// <param name="sendToUser">The callback (most likely used to broadcast a message)</param>
        public void EachUser(Action<Habbo> sendToUser)
        {
            foreach (Habbo session in GetSessions())
            {
                sendToUser(session);
            }
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

        public int GetUserCount()
        {
            return Users.Where(x => x.Type == EntityType.User).Count();
        }

        public bool HasUsers()
        {
            return Users.Any(x => x.Type == EntityType.User);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Timer.Dispose();
            }
        }

        private IEnumerable<Habbo> GetSessions()
        {
            // TODO Reimplement Users properly to prevent these queries!
            return Users.Where(x => x.Type == EntityType.User).Select(x => ((UserEntity) x).User);
        }

        private void OnTick(object state)
        {
            List<RoomEntity> changes = new List<RoomEntity>(this.Users.Count);

            foreach (RoomEntity entity in this.Users)
            {
                if (entity.HasSteps())
                {
                    entity.NextStep();
                }

                if (entity.NeedsUpdate)
                {
                    changes.Add(entity);
                }
            }

            this.OnTickCallback(this, changes);

            foreach (RoomEntity entity in changes)
            {
                entity.UpdateComplete();
            }
        }

        #endregion Methods
    }
}