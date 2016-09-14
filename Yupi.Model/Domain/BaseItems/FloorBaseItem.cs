namespace Yupi.Model.Domain
{
    using System;

    public class FloorBaseItem : BaseItem
    {
        #region Methods

        public override Item CreateNew()
        {
            return new SimpleFloorItem()
            {
                BaseItem = this
            };
        }

        #endregion Methods
    }
}