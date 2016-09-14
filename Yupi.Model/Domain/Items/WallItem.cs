namespace Yupi.Model.Domain
{
    using System;

    using Yupi.Model.Domain.Components;

    public abstract class WallItem : Item
    {
        #region Properties

        public virtual WallCoordinate Position
        {
            get; protected set;
        }

        #endregion Properties

        #region Methods

        public virtual string GetExtraData()
        {
            return string.Empty;
        }

        #endregion Methods
    }

    public abstract class WallItem<T> : WallItem
        where T : WallBaseItem
    {
        #region Properties

        public virtual T BaseItem
        {
            get; set;
        }

        #endregion Properties
    }
}