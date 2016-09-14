namespace Yupi.Model.Domain
{
    public class TrophyBaseItem : FloorBaseItem
    {
        public override Item CreateNew()
        {
            return new TrophyItem
            {
                BaseItem = this
            };
        }
    }
}