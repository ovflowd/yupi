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
    // TODO Refactor; Class might become blob!
    public class RoomManager
    {
        private List<Room> _loadedRooms;
        private IRepository<RoomData> RoomRepository;

        public IReadOnlyList<Room> LoadedRooms
        {
            get { return _loadedRooms.AsReadOnly(); }
        }

        public RoomManager()
        {
            _loadedRooms = new List<Room>();
            RoomRepository = DependencyFactory.Resolve<IRepository<RoomData>>();
        }

        public bool isLoaded(RoomData room)
        {
            return _loadedRooms.Any(x => x.Data == room);
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

        private void OnHumanEntityCreation(HumanEntity entity)
        {
            entity.OnDanceChange += OnDanceChange;
        }

        private void OnEntityCreation(RoomEntity entity)
        {
            entity.OnSleepChangeCB += OnSleepChange;
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

        private void OnSleepChange(RoomEntity entity)
        {
            entity.Room.EachUser(
                roomSession =>
                {
                    roomSession.Router.GetComposer<RoomUserIdleMessageComposer>()
                        .Compose(roomSession, entity.Id, entity.IsAsleep);
                });
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

        public Room GetIfLoaded(RoomData room)
        {
            return _loadedRooms.FirstOrDefault(x => x.Data == room);
        }

        public int UsersNow(RoomData data)
        {
            Room room = GetIfLoaded(data);
            return room == null ? 0 : room.GetUserCount();
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
    }
}