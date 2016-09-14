namespace Yupi.Model.Domain
{
    using System;

    public class MonsterPlantSeedBaseItem : FloorBaseItem
    {
        #region Methods

        public override Item CreateNew()
        {
            return new MonsterPlantSeedItem()
            {
                BaseItem = this
            };
        }

        #endregion Methods
    }
}