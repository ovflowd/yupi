namespace Yupi.Model.Domain
{
    public class PostItItem : WallItem<WallBaseItem>
    {
        public PostItItem()
        {
            Color = "FFFF33";
        }

        public virtual string Text { get; set; }

        // TODO Validate RGB-HTML string
        public virtual string Color { get; set; }

        public override string GetExtraData()
        {
            return Color;
        }
    }
}