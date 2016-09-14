namespace Yupi.Model.Domain
{
    using System;

    public class BadgeDisplayBaseItem : FloorBaseItem
    {
        #region Methods

        public override Item CreateNew()
        {
            return new BadgeDisplayItem()
            {
                BaseItem = this
            };
        }

        #endregion Methods
    }
}