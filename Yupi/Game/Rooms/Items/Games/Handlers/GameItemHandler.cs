using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Yupi.Data.Collections;
using Yupi.Game.Items.Interfaces;
using Yupi.Game.Rooms.User;

namespace Yupi.Game.Rooms.Items.Games.Handlers
{
    /// <summary>
    ///     Class GameItemHandler.
    /// </summary>
    internal class GameItemHandler
    {
        /// <summary>
        ///     The _banzai pyramids
        /// </summary>
        private ConcurrentDictionary<uint, RoomItem> _banzaiPyramids;

        /// <summary>
        ///     The _banzai teleports
        /// </summary>
        private QueuedDictionary<uint, RoomItem> _banzaiTeleports;

        /// <summary>
        ///     The _room
        /// </summary>
        private Room _room;

        /// <summary>
        ///     Initializes a new instance of the <see cref="GameItemHandler" /> class.
        /// </summary>
        /// <param name="room">The room.</param>
        public GameItemHandler(Room room)
        {
            _room = room;
            _banzaiPyramids = new ConcurrentDictionary<uint, RoomItem>();
            _banzaiTeleports = new QueuedDictionary<uint, RoomItem>();
        }

        /// <summary>
        ///     Called when [cycle].
        /// </summary>
        internal void OnCycle()
        {
            CyclePyramids();
            CycleRandomTeleports();
        }

        /// <summary>
        ///     Adds the pyramid.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="itemId">The item identifier.</param>
        internal void AddPyramid(RoomItem item, uint itemId)
        {
            if (_banzaiPyramids.ContainsKey(itemId))
            {
                _banzaiPyramids[itemId] = item;
                return;
            }
            _banzaiPyramids.TryAdd(itemId, item);
        }

        /// <summary>
        ///     Removes the pyramid.
        /// </summary>
        /// <param name="itemId">The item identifier.</param>
        internal void RemovePyramid(uint itemId)
        {
            RoomItem e;
            _banzaiPyramids.TryRemove(itemId, out e);
        }

        /// <summary>
        ///     Adds the teleport.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="itemId">The item identifier.</param>
        internal void AddTeleport(RoomItem item, uint itemId)
        {
            if (_banzaiTeleports.ContainsKey(itemId))
            {
                _banzaiTeleports.Inner[itemId] = item;
                return;
            }
            _banzaiTeleports.Add(itemId, item);
        }

        /// <summary>
        ///     Removes the teleport.
        /// </summary>
        /// <param name="itemId">The item identifier.</param>
        internal void RemoveTeleport(uint itemId)
        {
            _banzaiTeleports.Remove(itemId);
        }

        /// <summary>
        ///     Called when [teleport room user enter].
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="item">The item.</param>
        internal void OnTeleportRoomUserEnter(RoomUser user, RoomItem item)
        {
            List<RoomItem> items = _banzaiTeleports.Inner.Values.Where(p => p.Id != item.Id).ToList();

            if (!items.Any())
                return;

            int countId = Yupi.GetRandomNumber(0, items.Count());
            int countAmount = 0;

            foreach (RoomItem current in items.Where(current => current != null))
            {
                if (countAmount != countId)
                {
                    countAmount++;
                    continue;
                }
                current.ExtraData = "1";
                current.UpdateNeeded = true;
                _room.GetGameMap().TeleportToItem(user, current);
                item.ExtraData = "1";
                item.UpdateNeeded = true;
                current.UpdateState();
                item.UpdateState();

                break;
            }
        }

        /// <summary>
        ///     Destroys this instance.
        /// </summary>
        internal void Destroy()
        {
            if (_banzaiTeleports != null)
            {
                _banzaiTeleports.Destroy();
            }
            if (_banzaiPyramids != null)
            {
                _banzaiPyramids.Clear();
            }
            _banzaiPyramids = null;
            _banzaiTeleports = null;
            _room = null;
        }

        /// <summary>
        ///     Cycles the pyramids.
        /// </summary>
        private void CyclePyramids()
        {
            foreach (RoomItem item in _banzaiPyramids.Select(pyramid => pyramid.Value).Where(current => current != null))
            {
                if (item.InteractionCountHelper == 0 && item.ExtraData == "1")
                {
                    _room.GetGameMap().RemoveFromMap(item, false);
                    item.InteractionCountHelper = 1;
                }
                if (string.IsNullOrEmpty(item.ExtraData))
                    item.ExtraData = "0";

                int randomNumber = Yupi.GetRandomNumber(0, 30);
                if (randomNumber <= 26)
                    continue;
                if (item.ExtraData == "0")
                {
                    item.ExtraData = "1";
                    item.UpdateState();
                    _room.GetGameMap().RemoveFromMap(item, false);
                }
                else
                {
                    if (!_room.GetGameMap().ItemCanBePlacedHere(item.X, item.Y))
                        continue;
                    item.ExtraData = "0";
                    item.UpdateState();
                    _room.GetGameMap().AddItemToMap(item, false);
                }
            }
        }

        /// <summary>
        ///     Cycles the random teleports.
        /// </summary>
        private void CycleRandomTeleports()
        {
            _banzaiTeleports.OnCycle();
        }
    }
}