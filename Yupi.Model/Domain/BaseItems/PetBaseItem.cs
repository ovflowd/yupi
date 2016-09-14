namespace Yupi.Model.Domain
{
    using System;

    public class PetBaseItem : FloorBaseItem
    {
        #region Properties

        public override ItemType Type
        {
            get { return ItemType.Pet; }
        }

        #endregion Properties

        #region Methods

        public override Item CreateNew()
        {
            return new PetItem()
            {
                BaseItem = this
            };
        }

        #endregion Methods
    }
}