namespace Yupi.Model.Domain
{
    using System;

    public class MannequinBaseItem : FloorBaseItem
    {
        #region Methods

        public override Item CreateNew()
        {
            return new MannequinItem()
            {
                BaseItem = this
            };
        }

        #endregion Methods
    }
}