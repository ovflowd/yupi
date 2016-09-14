namespace Yupi.Model.Domain
{
    public class MusicDiscBaseItem : FloorBaseItem
    {
        public virtual SongData Song { get; protected set; }

        public override Item CreateNew()
        {
            return new SongItem
            {
                BaseItem = this
            };
        }
    }
}