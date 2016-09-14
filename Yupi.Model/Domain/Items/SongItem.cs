namespace Yupi.Model.Domain
{
    using System;

    // TODO SongItem vs MusicDiscItem
    public class SongItem : FloorItem<MusicDiscBaseItem>
    {
        #region Properties

        public virtual DateTime CreatedAt
        {
            get; set;
        }

        // TODO remove
        [Ignore]
        public virtual SongData Song
        {
            get { return BaseItem.Song; }
        }

        #endregion Properties

        #region Methods

        public override string GetExtraData()
        {
            return string.Join("\n", Owner.Name, CreatedAt.Year, CreatedAt.Month, CreatedAt.Day,
                Song.LengthSeconds, Song.Name);
        }

        public override void TryParseExtraData(string data)
        {
            CreatedAt = DateTime.Now;
        }

        #endregion Methods
    }
}