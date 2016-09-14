namespace Yupi.Model.Domain
{
    public class DimmerItem : WallItem<DimmerBaseItem>
    {
        public DimmerItem()
        {
            Data = new MoodlightData();
        }

        public virtual MoodlightData Data { get; set; }
    }
}