namespace Yupi.Model.Domain
{
    public class MannequinBaseItem : FloorBaseItem
    {
        public override Item CreateNew()
        {
            return new MannequinItem
            {
                BaseItem = this
            };
        }
    }
}