// ---------------------------------------------------------------------------------
// <copyright file="RoomManager.cs" company="https://github.com/sant0ro/Yupi">
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
namespace Yupi.Controller
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Yupi.Messages.Contracts;
    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;
    using Yupi.Util;

    // TODO Refactor; Class might become blob!
    public class RoomManager
    {
        #region Fields

        private IRepository<RoomData> RoomRepository;
        private List<Room> _loadedRooms;

        #endregion Fields

        #region Properties

        public IReadOnlyList<Room> LoadedRooms
        {
            get { return _loadedRooms.AsReadOnly(); }
        }

        #endregion Properties

        #region Constructors

        public RoomManager()
        {
            _loadedRooms = new List<Room>();
            RoomRepository = DependencyFactory.Resolve<IRepository<RoomData>>();
        }

        #endregion Constructors

        #region Methods

        public IOrderedEnumerable<Room> GetActive()
        {
            return LoadedRooms
                .Where(x => x.GetUserCount() > 0)
                .OrderByDescending(x => x.GetUserCount());
        }

        public IEnumerable<RoomData> GetEventRooms()
        {
            return RoomRepository
                .FilterBy(x => x.Event != null && !x.Event.HasExpired())
                .OrderByDescending(x => UsersNow(x))
                .AsEnumerable();
        }

        public Room GetIfLoaded(RoomData room)
        {
            return _loadedRooms.FirstOrDefault(x => x.Data == room);
        }

        public bool isLoaded(RoomData room)
        {
            return _loadedRooms.Any(x => x.Data == room);
        }

        public void KickAll(Room room)
        {
            room.EachUser(
                (session) =>
                {
                    if (session.Info.HasPermission("ignore_room_kick"))
                    {
                        return;
                    }

                    session.Router.GetComposer<OutOfRoomMessageComposer>()
                        .Compose(session, 2);

                    RemoveUser(session);
                }
            );
        }

        public Room LoadOrGet(int roomId)
        {
            RoomData data = RoomRepository.FindBy(roomId);
            return LoadOrGet(data);
        }

        public Room LoadOrGet(RoomData data)
        {
            if (data == null)
            {
                return null;
            }

            Room room = GetIfLoaded(data);

            if (room == null)
            {
                room = new Room(data, OnRoomTick)
                {
                    OnEntityCreateCallback = OnEntityCreation,
                    OnHumanEntityCreate = OnHumanEntityCreation
                };
                _loadedRooms.Add(room);
            }

            return room;
        }

        public void RemoveUser(Habbo session)
        {
            if (session.Room == null)
            {
                return;
            }

            session.Room.Users.Remove(session.RoomEntity);

            // TODO Stop effect
            // TODO Stop trade

            // TODO Update Room Users?!

            session.IsRidingHorse = false;

            session.RoomEntity = null;
        }

        public int UsersNow(RoomData data)
        {
            Room room = GetIfLoaded(data);
            return room == null ? 0 : room.GetUserCount();
        }

        private void OnDanceChange(HumanEntity entity)
        {
            entity.Room.EachUser(
                roomSession =>
                {
                    roomSession.Router.GetComposer<DanceStatusMessageComposer>()
                        .Compose(roomSession, entity.Id, entity.Dance);
                });
        }

        private void OnEntityCreation(RoomEntity entity)
        {
            entity.OnSleepChangeCB += OnSleepChange;
        }

        private void OnHumanEntityCreation(HumanEntity entity)
        {
            entity.OnDanceChange += OnDanceChange;
        }

        private void OnRoomTick(Room room, List<RoomEntity> changes)
        {
            if (changes.Count == 0)
            {
                return;
            }

            room.EachUser(
                (session) =>
                {
                    session.Router.GetComposer<UpdateUserStatusMessageComposer>()
                        .Compose(session, changes);
                }
            );
        }

        private void OnSleepChange(RoomEntity entity)
        {
            entity.Room.EachUser(
                roomSession =>
                {
                    roomSession.Router.GetComposer<RoomUserIdleMessageComposer>()
                        .Compose(roomSession, entity.Id, entity.IsAsleep);
                });
        }

        #endregion Methods
    }
}