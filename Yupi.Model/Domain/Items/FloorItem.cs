using System;

namespace Yupi.Model.Domain
{
    public abstract class FloorItem : Item
    {
    }

    public abstract class FloorItem<T> : FloorItem where T : FloorBaseItem
    {
        public virtual T BaseItem { get; set; }

        public virtual string GetExtraData()
        {
            return string.Empty;
        }
    }
}