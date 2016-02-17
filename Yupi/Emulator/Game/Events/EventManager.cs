using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Yupi.Game.Events.Interfaces;
using Yupi.Game.Rooms.Data;

namespace Yupi.Game.Events
{
    /// <summary>
    ///     Class EventManager.
    /// </summary>
    internal class EventManager
    {
        /// <summary>
        ///     The _add queue
        /// </summary>
        private readonly Queue _addQueue;

        /// <summary>
        ///     The _event categories
        /// </summary>
        private readonly Dictionary<int, EventCategory> _eventCategories;

        /// <summary>
        ///     The _events
        /// </summary>
        private readonly Dictionary<RoomData, uint> _events;

        /// <summary>
        ///     The _remove queue
        /// </summary>
        private readonly Queue _removeQueue;

        /// <summary>
        ///     The _update queue
        /// </summary>
        private readonly Queue _updateQueue;

        /// <summary>
        ///     The _ordered event rooms
        /// </summary>
        private IOrderedEnumerable<KeyValuePair<RoomData, uint>> _orderedEventRooms;

        /// <summary>
        ///     Initializes a new instance of the <see cref="EventManager" /> class.
        /// </summary>
        public EventManager()
        {
            _eventCategories = new Dictionary<int, EventCategory>();
            _events = new Dictionary<RoomData, uint>();

            _orderedEventRooms = _events.OrderByDescending(t => t.Value);

            _addQueue = new Queue();
            _removeQueue = new Queue();
            _updateQueue = new Queue();

            for (int i = 0; i < 30; i++)
                _eventCategories.Add(i, new EventCategory(i));
        }

        /// <summary>
        ///     Gets the rooms.
        /// </summary>
        /// <returns>KeyValuePair&lt;RoomData, System.UInt32&gt;[].</returns>
        internal KeyValuePair<RoomData, uint>[] GetRooms() => _orderedEventRooms.ToArray();

        /// <summary>
        ///     Called when [cycle].
        /// </summary>
        internal void OnCycle()
        {
            WorkRemoveQueue();
            WorkAddQueue();
            WorkUpdate();
            SortCollection();

            foreach (EventCategory current in _eventCategories.Values)
                current.OnCycle();
        }

        /// <summary>
        ///     Queues the add event.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="roomEventCategory">The room event category.</param>
        internal void QueueAddEvent(RoomData data, int roomEventCategory)
        {
            lock (_addQueue.SyncRoot)
                _addQueue.Enqueue(data);

            _eventCategories[roomEventCategory].QueueAddEvent(data);
        }

        /// <summary>
        ///     Queues the remove event.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="roomEventCategory">The room event category.</param>
        internal void QueueRemoveEvent(RoomData data, int roomEventCategory)
        {
            lock (_removeQueue.SyncRoot)
                _removeQueue.Enqueue(data);

            _eventCategories[roomEventCategory].QueueRemoveEvent(data);
        }

        /// <summary>
        ///     Queues the update event.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="roomEventCategory">The room event category.</param>
        internal void QueueUpdateEvent(RoomData data, int roomEventCategory)
        {
            lock (_updateQueue.SyncRoot)
                _updateQueue.Enqueue(data);

            _eventCategories[roomEventCategory].QueueUpdateEvent(data);
        }

        /// <summary>
        ///     Sorts the collection.
        /// </summary>
        private void SortCollection()
        {
            _orderedEventRooms = _events.Take(40).OrderByDescending(t => t.Value);
        }

        /// <summary>
        ///     Works the add queue.
        /// </summary>
        private void WorkAddQueue()
        {
            if (_addQueue.Count <= 0)
                return;

            lock (_addQueue.SyncRoot)
            {
                while (_addQueue.Count > 0)
                {
                    RoomData roomData = (RoomData) _addQueue.Dequeue();

                    if (!_events.ContainsKey(roomData))
                        _events.Add(roomData, roomData.UsersNow);
                }
            }
        }

        /// <summary>
        ///     Works the remove queue.
        /// </summary>
        private void WorkRemoveQueue()
        {
            if (_removeQueue.Count <= 0)
                return;

            lock (_removeQueue.SyncRoot)
            {
                while (_removeQueue.Count > 0)
                    _events.Remove((RoomData) _removeQueue.Dequeue());
            }
        }

        /// <summary>
        ///     Works the update.
        /// </summary>
        private void WorkUpdate()
        {
            if (_removeQueue.Count <= 0)
                return;

            lock (_removeQueue.SyncRoot)
            {
                while (_removeQueue.Count > 0)
                {
                    RoomData roomData = (RoomData) _updateQueue.Dequeue();

                    if (_events.ContainsKey(roomData))
                        _events[roomData] = roomData.UsersNow;
                }
            }
        }
    }
}