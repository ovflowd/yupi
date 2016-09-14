namespace Yupi.Model.Domain
{
    using System;

    public class TrophyBaseItem : FloorBaseItem
    {
        #region Methods

        public override Item CreateNew()
        {
            return new TrophyItem()
            {
                BaseItem = this
            };
        }

        #endregion Methods
    }
}