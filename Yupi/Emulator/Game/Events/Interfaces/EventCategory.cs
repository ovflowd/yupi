using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Yupi.Game.Rooms.Data;

namespace Yupi.Game.Events.Interfaces
{
    /// <summary>
    ///     Class EventCategory.
    /// </summary>
    internal class EventCategory
    {
        /// <summary>
        ///     The _add queue
        /// </summary>
        private readonly Queue _addQueue;

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
        ///     Initializes a new instance of the <see cref="EventCategory" /> class.
        /// </summary>
        /// <param name="categoryId">The category identifier.</param>
        internal EventCategory(int categoryId)
        {
            CategoryId = categoryId;
            _events = new Dictionary<RoomData, uint>();
            _orderedEventRooms = from t in _events orderby t.Value descending select t;
            _addQueue = new Queue();
            _removeQueue = new Queue();
            _updateQueue = new Queue();
        }

        /// <summary>
        ///     The _category identifier
        /// </summary>
        public int CategoryId { get; }

        /// <summary>
        ///     Gets the active rooms.
        /// </summary>
        /// <returns>KeyValuePair&lt;RoomData, System.UInt32&gt;[].</returns>
        internal KeyValuePair<RoomData, uint>[] GetActiveRooms()
        {
            return _orderedEventRooms.ToArray();
        }

        /// <summary>
        ///     Called when [cycle].
        /// </summary>
        internal void OnCycle()
        {
            WorkRemoveQueue();
            WorkAddQueue();
            WorkUpdate();
            SortCollection();
        }

        /// <summary>
        ///     Queues the add event.
        /// </summary>
        /// <param name="data">The data.</param>
        internal void QueueAddEvent(RoomData data)
        {
            lock (_addQueue.SyncRoot)
                _addQueue.Enqueue(data);
        }

        /// <summary>
        ///     Queues the remove event.
        /// </summary>
        /// <param name="data">The data.</param>
        internal void QueueRemoveEvent(RoomData data)
        {
            lock (_removeQueue.SyncRoot)
                _removeQueue.Enqueue(data);
        }

        /// <summary>
        ///     Queues the update event.
        /// </summary>
        /// <param name="data">The data.</param>
        internal void QueueUpdateEvent(RoomData data)
        {
            lock (_updateQueue.SyncRoot)
                _updateQueue.Enqueue(data);
        }

        /// <summary>
        ///     Sorts the collection.
        /// </summary>
        private void SortCollection()
        {
            _orderedEventRooms =
                from t in _events.Take(40)
                orderby t.Value descending
                select t;
        }

        /// <summary>
        ///     Works the add queue.
        /// </summary>
        private void WorkAddQueue()
        {
            if (_addQueue == null || _addQueue.Count <= 0)
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
            if (_removeQueue == null || _removeQueue.Count <= 0)
                return;

            lock (_removeQueue.SyncRoot)
            {
                while (_removeQueue.Count > 0)
                {
                    RoomData key = (RoomData) _removeQueue.Dequeue();
                    _events.Remove(key);
                }
            }
        }

        /// <summary>
        ///     Works the update.
        /// </summary>
        private void WorkUpdate()
        {
            if (_removeQueue == null || _removeQueue.Count <= 0)
                return;

            lock (_removeQueue.SyncRoot)
            {
                while (_removeQueue.Count > 0)
                {
                    RoomData roomData = (RoomData) _updateQueue.Dequeue();

                    if (!_events.ContainsKey(roomData))
                        _events.Add(roomData, roomData.UsersNow);
                    else
                        _events[roomData] = roomData.UsersNow;
                }
            }
        }
    }
}