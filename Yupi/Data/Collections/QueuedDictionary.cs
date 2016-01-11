/**
     Because i love chocolat...                                      
                                    88 88  
                                    "" 88  
                                       88  
8b       d8 88       88 8b,dPPYba,  88 88  
`8b     d8' 88       88 88P'    "8a 88 88  
 `8b   d8'  88       88 88       d8 88 ""  
  `8b,d8'   "8a,   ,a88 88b,   ,a8" 88 aa  
    Y88'     `"YbbdP'Y8 88`YbbdP"'  88 88  
    d8'                 88                 
   d8'                  88     
   
   Private Habbo Hotel Emulating System
   @author Claudio A. Santoro W.
   @author Kessiler R.
   @version dev-beta
   @license MIT
   @copyright Sulake Corporation Oy
   @observation All Rights of Habbo, Habbo Hotel, and all Habbo contents and it's names, is copyright from Sulake
   Corporation Oy. Yupi! has nothing linked with Sulake. 
   This Emulator is Only for DEVELOPMENT uses. If you're selling this you're violating Sulakes Copyright.
*/

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Yupi.Data.Collections
{
    public class QueuedDictionary<T, TV>
    {
        private ConcurrentQueue<KeyValuePair<T, TV>> _addQueue;
        private EventHandler _onAdd;
        private EventHandler _onCycleDone;
        private ConcurrentQueue<OnCycleDoneDelegate> _onCycleEventQueue;
        private EventHandler _onRemove;
        private EventHandler _onUpdate;
        private ConcurrentQueue<T> _removeQueue;
        private ConcurrentQueue<KeyValuePair<T, TV>> _updateQueue;

        public QueuedDictionary()
        {
            Inner = new ConcurrentDictionary<T, TV>();
            _addQueue = new ConcurrentQueue<KeyValuePair<T, TV>>();
            _updateQueue = new ConcurrentQueue<KeyValuePair<T, TV>>();
            _removeQueue = new ConcurrentQueue<T>();
            _onCycleEventQueue = new ConcurrentQueue<OnCycleDoneDelegate>();
        }

        public QueuedDictionary(EventHandler onAddItem, EventHandler onUpdate, EventHandler onRemove,
            EventHandler onCycleDone)
        {
            Inner = new ConcurrentDictionary<T, TV>();
            _addQueue = new ConcurrentQueue<KeyValuePair<T, TV>>();
            _updateQueue = new ConcurrentQueue<KeyValuePair<T, TV>>();
            _removeQueue = new ConcurrentQueue<T>();
            _onAdd = onAddItem;
            _onUpdate = onUpdate;
            _onRemove = onRemove;
            _onCycleDone = onCycleDone;
            _onCycleEventQueue = new ConcurrentQueue<OnCycleDoneDelegate>();
        }

        public ICollection<TV> Values => Inner.Values;

        public ICollection<T> Keys => Inner.Keys;

        public ConcurrentDictionary<T, TV> Inner { get; set; }

        public void OnCycle()
        {
            WorkRemoveQueue();
            WorkAddQueue();
            WorkUpdateQueue();
            WorkOnEventDoneQueue();
            _onCycleDone?.Invoke(null, new EventArgs());
        }

        public void Add(T key, TV value)
        {
            KeyValuePair<T, TV> keyValuePair = new KeyValuePair<T, TV>(key, value);

            _addQueue.Enqueue(keyValuePair);
        }

        public void Update(T key, TV value)
        {
            KeyValuePair<T, TV> keyValuePair = new KeyValuePair<T, TV>(key, value);

            _updateQueue.Enqueue(keyValuePair);
        }

        public void Remove(T key) => _removeQueue.Enqueue(key);

        public TV GetValue(T key) => Inner.ContainsKey(key) ? Inner[key] : default(TV);

        public bool ContainsKey(T key) => Inner.ContainsKey(key);

        public void Clear() => Inner.Clear();

        public void QueueDelegate(OnCycleDoneDelegate function) => _onCycleEventQueue.Enqueue(function);

        public List<KeyValuePair<T, TV>> ToList() => Inner.ToList();

        public void Destroy()
        {
            Inner?.Clear();

            if (_addQueue?.Any() ?? false)
            {
                KeyValuePair<T, TV> item;

                while (_addQueue.TryDequeue(out item))
                    continue;
            }

            if (_updateQueue?.Any() ?? false)
            {
                KeyValuePair<T, TV> item;

                while (_updateQueue.TryDequeue(out item))
                    continue;
            }

            if (_removeQueue?.Any() ?? false)
            {
                T item;

                while (_removeQueue.TryDequeue(out item))
                    continue;
            }

            if (_onCycleEventQueue?.Any() ?? false)
            {
                OnCycleDoneDelegate item;

                while (_onCycleEventQueue.TryDequeue(out item))
                    continue;
            }

            Inner = null;
            _addQueue = null;
            _updateQueue = null;
            _removeQueue = null;
            _onCycleEventQueue = null;
            _onAdd = null;
            _onUpdate = null;
            _onRemove = null;
            _onCycleDone = null;
        }

        private void WorkOnEventDoneQueue()
        {
            if (_onCycleEventQueue.Any())
                return;

            OnCycleDoneDelegate item;

            while (_onCycleEventQueue.TryDequeue(out item))
                item();
        }

        private void WorkAddQueue()
        {
            if (!_addQueue.Any())
                return;

            KeyValuePair<T, TV> item;

            while (_addQueue.TryDequeue(out item))
            {
                if (Inner.ContainsKey(item.Key))
                    Inner[item.Key] = item.Value;
                else
                    Inner.TryAdd(item.Key, item.Value);

                _onAdd?.Invoke(item, null);
            }
        }

        private void WorkUpdateQueue()
        {
            if (!_updateQueue.Any())
                return;

            KeyValuePair<T, TV> item;

            while (_updateQueue.TryDequeue(out item))
            {
                if (Inner.ContainsKey(item.Key))
                    Inner[item.Key] = item.Value;
                else
                    Inner.TryAdd(item.Key, item.Value);

                _onUpdate?.Invoke(item, null);
            }
        }

        private void WorkRemoveQueue()
        {
            if (!_removeQueue.Any())
                return;

            List<T> list = new List<T>();

            T item;

            while (_removeQueue.TryDequeue(out item))
            {
                if (Inner.ContainsKey(item))
                {
                    TV value = Inner[item];

                    TV junkItem;

                    Inner.TryRemove(item, out junkItem);

                    KeyValuePair<T, TV> keyValuePair = new KeyValuePair<T, TV>(item, value);

                    _onRemove?.Invoke(keyValuePair, null);
                }
                else
                    list.Add(item);
            }

            if (!list.Any())
                return;

            foreach (T current in list)
                _removeQueue.Enqueue(current);
        }
    }
}