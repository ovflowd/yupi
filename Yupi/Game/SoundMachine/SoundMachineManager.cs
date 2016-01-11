using System.Collections.Generic;
using System.Linq;
using Yupi.Game.Items.Interfaces;
using Yupi.Game.Rooms;
using Yupi.Game.Rooms.User;
using Yupi.Game.SoundMachine.Composers;
using Yupi.Game.SoundMachine.Songs;

namespace Yupi.Game.SoundMachine
{
    /// <summary>
    ///     Class RoomMusicController.
    /// </summary>
    internal class SoundMachineManager
    {
        /// <summary>
        ///     The _m broadcast needed
        /// </summary>
        private static bool _mBroadcastNeeded;

        /// <summary>
        ///     The _m loaded disks
        /// </summary>
        private Dictionary<uint, SongItem> _mLoadedDisks;

        /// <summary>
        ///     The _m playlist
        /// </summary>
        private SortedDictionary<int, SongInstance> _mPlaylist;

        /// <summary>
        ///     The _m room output item
        /// </summary>
        private RoomItem _mRoomOutputItem;

        /// <summary>
        ///     The _m started playing timestamp
        /// </summary>
        private double _mStartedPlayingTimestamp;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SoundMachineManager" /> class.
        /// </summary>
        public SoundMachineManager()
        {
            _mLoadedDisks = new Dictionary<uint, SongItem>();
            _mPlaylist = new SortedDictionary<int, SongInstance>();
        }

        /// <summary>
        ///     Gets the current song.
        /// </summary>
        /// <value>The current song.</value>
        public SongInstance CurrentSong { get; private set; }

        /// <summary>
        ///     Gets a value indicating whether this instance is playing.
        /// </summary>
        /// <value><c>true</c> if this instance is playing; otherwise, <c>false</c>.</value>
        public bool IsPlaying { get; private set; }

        /// <summary>
        ///     Gets the time playing.
        /// </summary>
        /// <value>The time playing.</value>
        public double TimePlaying => Yupi.GetUnixTimeStamp() - _mStartedPlayingTimestamp;

        /// <summary>
        ///     Gets the song synchronize timestamp.
        /// </summary>
        /// <value>The song synchronize timestamp.</value>
        public int SongSyncTimestamp
        {
            get
            {
                if (!IsPlaying || CurrentSong == null)
                    return 0;

                if (TimePlaying >= CurrentSong.SongData.LengthSeconds)
                    return (int) CurrentSong.SongData.LengthSeconds;

                return (int) (TimePlaying*1000.0);
            }
        }

        /// <summary>
        ///     Gets the playlist.
        /// </summary>
        /// <value>The playlist.</value>
        public SortedDictionary<int, SongInstance> Playlist
        {
            get
            {
                SortedDictionary<int, SongInstance> sortedDictionary = new SortedDictionary<int, SongInstance>();

                lock (_mPlaylist)
                {
                    foreach (KeyValuePair<int, SongInstance> current in _mPlaylist)
                        sortedDictionary.Add(current.Key, current.Value);
                }

                return sortedDictionary;
            }
        }

        /// <summary>
        ///     Gets the playlist capacity.
        /// </summary>
        /// <value>The playlist capacity.</value>
        public int PlaylistCapacity => 20;

        /// <summary>
        ///     Gets the size of the playlist.
        /// </summary>
        /// <value>The size of the playlist.</value>
        public int PlaylistSize => _mPlaylist.Count;

        /// <summary>
        ///     Gets a value indicating whether this instance has linked item.
        /// </summary>
        /// <value><c>true</c> if this instance has linked item; otherwise, <c>false</c>.</value>
        public bool HasLinkedItem => _mRoomOutputItem != null;

        /// <summary>
        ///     Gets the linked item identifier.
        /// </summary>
        /// <value>The linked item identifier.</value>
        public uint LinkedItemId => _mRoomOutputItem?.Id ?? 0u;

        /// <summary>
        ///     Gets the song queue position.
        /// </summary>
        /// <value>The song queue position.</value>
        public int SongQueuePosition { get; private set; }

        /// <summary>
        ///     Links the room output item.
        /// </summary>
        /// <param name="item">The item.</param>
        public void LinkRoomOutputItem(RoomItem item)
        {
            _mRoomOutputItem = item;
        }

        /// <summary>
        ///     Adds the disk.
        /// </summary>
        /// <param name="diskItem">The disk item.</param>
        /// <returns>System.Int32.</returns>
        public int AddDisk(SongItem diskItem)
        {
            uint songId = diskItem.SongId;

            if (songId == 0u)
                return -1;

            SongData song = SoundMachineSongManager.GetSong(songId);

            if (song == null)
                return -1;

            lock (_mLoadedDisks)
                if (_mLoadedDisks.ContainsKey(diskItem.ItemId))
                    return -1;

            lock (_mLoadedDisks)
                _mLoadedDisks.Add(diskItem.ItemId, diskItem);

            int count = _mPlaylist.Count;

            lock (_mPlaylist)
                _mPlaylist.Add(count, new SongInstance(diskItem, song));

            return count;
        }

        /// <summary>
        ///     Removes the disk.
        /// </summary>
        /// <param name="playlistIndex">Index of the playlist.</param>
        /// <returns>SongItem.</returns>
        public SongItem RemoveDisk(int playlistIndex)
        {
            SongInstance songInstance;
            lock (_mPlaylist)
            {
                if (!_mPlaylist.ContainsKey(playlistIndex))
                    return null;

                songInstance = _mPlaylist[playlistIndex];

                _mPlaylist.Remove(playlistIndex);
            }

            lock (_mLoadedDisks)
                _mLoadedDisks.Remove(songInstance.DiskItem.ItemId);

            RepairPlaylist();

            if (playlistIndex == SongQueuePosition)
                PlaySong();

            return songInstance.DiskItem;
        }

        /// <summary>
        ///     Updates the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public void Update(Room instance)
        {
            if (IsPlaying && (CurrentSong == null || TimePlaying >= CurrentSong.SongData.LengthSeconds + 1.0))
            {
                lock (_mPlaylist)
                {
                    if (!_mPlaylist.Any())
                    {
                        Stop();
                        _mRoomOutputItem.ExtraData = "0";
                        _mRoomOutputItem.UpdateState();
                    }
                    else
                        SetNextSong();
                }

                _mBroadcastNeeded = true;
            }

            if (!_mBroadcastNeeded)
                return;

            BroadcastCurrentSongData(instance);
            _mBroadcastNeeded = false;
        }

        /// <summary>
        ///     Repairs the playlist.
        /// </summary>
        public void RepairPlaylist()
        {
            List<SongItem> list;

            lock (_mLoadedDisks)
            {
                list = _mLoadedDisks.Values.ToList();
                _mLoadedDisks.Clear();
            }

            lock (_mPlaylist)
                _mPlaylist.Clear();

            foreach (SongItem current in list)
                AddDisk(current);
        }

        /// <summary>
        ///     Sets the next song.
        /// </summary>
        public void SetNextSong()
        {
            SongQueuePosition++;
            PlaySong();
        }

        /// <summary>
        ///     Plays the song.
        /// </summary>
        public void PlaySong()
        {
            if (SongQueuePosition >= _mPlaylist.Count)
                SongQueuePosition = 0;

            lock (_mPlaylist)
            {
                if (!_mPlaylist.Any())
                {
                    Stop();
                    return;
                }
            }

            lock (_mPlaylist)
                CurrentSong = _mPlaylist[SongQueuePosition];

            _mStartedPlayingTimestamp = Yupi.GetUnixTimeStamp();
            _mBroadcastNeeded = true;
        }

        /// <summary>
        ///     Starts this instance.
        /// </summary>
        public void Start()
        {
            IsPlaying = true;
            SongQueuePosition = -1;
            SetNextSong();
        }

        /// <summary>
        ///     Stops this instance.
        /// </summary>
        public void Stop()
        {
            CurrentSong = null;
            IsPlaying = false;
            SongQueuePosition = -1;
            _mBroadcastNeeded = true;
        }

        /// <summary>
        ///     Resets this instance.
        /// </summary>
        public void Reset()
        {
            lock (_mLoadedDisks)
                _mLoadedDisks.Clear();

            lock (_mPlaylist)
                _mPlaylist.Clear();

            _mRoomOutputItem = null;
            SongQueuePosition = -1;
            _mStartedPlayingTimestamp = 0.0;
        }

        /// <summary>
        ///     Broadcasts the current song data.
        /// </summary>
        /// <param name="instance">The instance.</param>
        internal void BroadcastCurrentSongData(Room instance)
        {
            if (CurrentSong != null)
            {
                instance.SendMessage(SoundMachineComposer.ComposePlayingComposer(CurrentSong.SongData.Id,
                    SongQueuePosition, 0));
                return;
            }

            instance.SendMessage(SoundMachineComposer.ComposePlayingComposer(0u, 0, 0));
        }

        /// <summary>
        ///     Called when [new user enter].
        /// </summary>
        /// <param name="user">The user.</param>
        internal void OnNewUserEnter(RoomUser user)
        {
            if (user.IsBot || user.GetClient() == null || CurrentSong == null)
                return;

            user.GetClient()
                .SendMessage(SoundMachineComposer.ComposePlayingComposer(CurrentSong.SongData.Id, SongQueuePosition,
                    SongSyncTimestamp));
        }

        /// <summary>
        ///     Destroys this instance.
        /// </summary>
        internal void Destroy()
        {
            lock (_mLoadedDisks)
                _mLoadedDisks?.Clear();

            lock (_mPlaylist)
                _mPlaylist?.Clear();

            lock (_mPlaylist)
                _mPlaylist = null;

            lock (_mLoadedDisks)
                _mLoadedDisks = null;

            CurrentSong = null;
            _mRoomOutputItem = null;
        }
    }
}