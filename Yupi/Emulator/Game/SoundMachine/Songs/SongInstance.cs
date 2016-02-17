namespace Yupi.Game.SoundMachine.Songs
{
    /// <summary>
    ///     Class SongInstance.
    /// </summary>
    internal class SongInstance
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="SongInstance" /> class.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="songData">The song data.</param>
        public SongInstance(SongItem item, SongData songData)
        {
            DiskItem = item;
            SongData = songData;
        }

        /// <summary>
        ///     Gets the disk item.
        /// </summary>
        /// <value>The disk item.</value>
        public SongItem DiskItem { get; private set; }

        /// <summary>
        ///     Gets the song data.
        /// </summary>
        /// <value>The song data.</value>
        public SongData SongData { get; private set; }
    }
}