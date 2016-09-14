namespace Yupi.Model.Domain
{
    using System;

    public class MusicDiscBaseItem : FloorBaseItem
    {
        #region Properties

        public virtual SongData Song
        {
            get; protected set;
        }

        #endregion Properties

        #region Methods

        public override Item CreateNew()
        {
            return new SongItem()
            {
                BaseItem = this
            };
        }

        #endregion Methods
    }
}