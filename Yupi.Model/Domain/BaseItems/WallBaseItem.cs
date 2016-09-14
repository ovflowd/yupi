namespace Yupi.Model.Domain
{
    using System;

    public class WallBaseItem : BaseItem
    {
        #region Properties

        public override ItemType Type
        {
            get { return ItemType.Wall; }
        }

        #endregion Properties

        #region Methods

        public override Item CreateNew()
        {
            return new SimpleWallItem()
            {
                BaseItem = this
            };
        }

        #endregion Methods
    }
}