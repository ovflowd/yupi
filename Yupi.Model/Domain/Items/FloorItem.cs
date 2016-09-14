namespace Yupi.Model.Domain
{
    using System;

    public abstract class FloorItem : Item
    {
    }

    public abstract class FloorItem<T> : FloorItem
        where T : FloorBaseItem
    {
        #region Properties

        public virtual T BaseItem
        {
            get; set;
        }

        #endregion Properties

        #region Methods

        public virtual string GetExtraData()
        {
            return string.Empty;
        }

        #endregion Methods
    }
}