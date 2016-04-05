using System.Collections.Generic;
using System.Data;
using Yupi.Emulator.Data.Base.Adapters.Interfaces;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Rooms.Events.Composers;
using Yupi.Emulator.Game.Rooms.Events.Models;

namespace Yupi.Emulator.Game.Rooms.Events
{
    /// <summary>
    ///     Class RoomEventsManager.
    /// </summary>
     class RoomEventsManager
    {
        /// <summary>
        ///     The Room Events
        /// </summary>
        private readonly Dictionary<uint, RoomEvent> _events;

        /// <summary>
        ///     Initializes a new instance of the <see cref="RoomEventsManager" /> class.
        /// </summary>
         RoomEventsManager()
        {
            _events = new Dictionary<uint, RoomEvent>();

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.SetQuery("SELECT * FROM rooms_events WHERE `expire` > UNIX_TIMESTAMP()");

                DataTable table = queryReactor.GetTable();

                foreach (DataRow dataRow in table.Rows)
                {
                    _events.Add((uint) dataRow["room_id"],
                        new RoomEvent((uint) dataRow["room_id"], dataRow["name"].ToString(), dataRow["description"].ToString(), (int) dataRow["expire"],
                            (int) dataRow["category"]));
                }
            }
        }

        /// <summary>
        ///     Adds the new event.
        /// </summary>
        /// <param name="roomId">The room identifier.</param>
        /// <param name="eventName">Name of the event.</param>
        /// <param name="eventDesc">The event desc.</param>
        /// <param name="session">The session.</param>
        /// <param name="time">The time.</param>
        /// <param name="category">The category.</param>
         void AddNewEvent(uint roomId, string eventName, string eventDesc, GameClient session, int time = 7200, int category = 1)
        {
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(roomId);

            RoomEvent roomEvent = new RoomEvent(roomId, eventName, eventDesc)
            {
                Name = eventName,

                Description = eventDesc
            };

            roomEvent.Time = roomEvent.HasExpired ? Yupi.GetUnixTimeStamp() + time : roomEvent.Time + time;

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.SetQuery("REPLACE INTO rooms_events VALUES (@id,@name,@desc,@time,@category)");

                queryReactor.AddParameter("id", roomId);
                queryReactor.AddParameter("name", eventName);
                queryReactor.AddParameter("desc", eventDesc);
                queryReactor.AddParameter("time", roomEvent.Time);
                queryReactor.AddParameter("category", category);

                queryReactor.RunQuery();
            }

            if (!_events.ContainsKey(roomId))
                _events.Add(roomId, roomEvent);
            else
                _events[roomId] = roomEvent;

            Yupi.GetGame().GetRoomManager().GenerateRoomData(roomId).Event = roomEvent;

            if (room != null)
                room.RoomData.Event = roomEvent;

            if (session.GetHabbo().CurrentRoomId == roomId)
                SerializeEventInfo(roomId);
        }

        /// <summary>
        ///     Removes the event.
        /// </summary>
        /// <param name="roomId">The room identifier.</param>
         void RemoveEvent(uint roomId)
        {
            _events.Remove(roomId);

            SerializeEventInfo(roomId);
        }

        /// <summary>
        ///     Gets the events.
        /// </summary>
        /// <returns>Dictionary&lt;System.UInt32, RoomEvent&gt;.</returns>
         Dictionary<uint, RoomEvent> GetEvents() => _events;

        /// <summary>
        ///     Gets the event.
        /// </summary>
        /// <param name="roomId">The room identifier.</param>
        /// <returns>RoomEvent.</returns>
         RoomEvent GetEvent(uint roomId) => _events.ContainsKey(roomId) ? _events[roomId] : null;

        /// <summary>
        ///     Rooms the has events.
        /// </summary>
        /// <param name="roomId">The room identifier.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
         bool RoomHasEvents(uint roomId) => _events.ContainsKey(roomId);

        /// <summary>
        ///     Serializes the event information.
        /// </summary>
        /// <param name="roomId">The room identifier.</param>
         void SerializeEventInfo(uint roomId)
        {
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(roomId);

            if (room == null)
                return;

            RoomEvent roomEvent = GetEvent(roomId);

            if (roomEvent == null || roomEvent.HasExpired)
                return;

            if (!RoomHasEvents(roomId))
                return;

            room.SendMessage(RoomEventComposer.Compose(roomEvent, room));
        }

        /// <summary>
        ///     Updates the event.
        /// </summary>
        /// <param name="Event">The event.</param>
         void UpdateEvent(RoomEvent Event)
        {
            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.SetQuery(string.Concat("REPLACE INTO rooms_events VALUES (", Event.RoomId,", @name, @desc, ", Event.Time, ")"));
                queryReactor.AddParameter("name", Event.Name);
                queryReactor.AddParameter("desc", Event.Description);
                queryReactor.RunQuery();
            }

            SerializeEventInfo(Event.RoomId);
        }
    }
}