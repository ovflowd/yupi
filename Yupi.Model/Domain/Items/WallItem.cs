using Yupi.Model.Domain.Components;

namespace Yupi.Model.Domain
{
    public abstract class WallItem : Item
    {
        public virtual WallCoordinate Position { get; protected set; }

        public virtual string GetExtraData()
        {
            return string.Empty;
        }
    }

    public abstract class WallItem<T> : WallItem where T : WallBaseItem
    {
        public virtual T BaseItem { get; set; }
    }
}