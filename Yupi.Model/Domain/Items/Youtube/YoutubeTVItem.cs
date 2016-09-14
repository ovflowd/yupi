namespace Yupi.Model.Domain
{
    using System;

    public class YoutubeTVItem : FloorItem<YoutubeTvBaseItem>
    {
        #region Properties

        public virtual YoutubeVideo PlayingVideo
        {
            get; set;
        }

        #endregion Properties
    }
}