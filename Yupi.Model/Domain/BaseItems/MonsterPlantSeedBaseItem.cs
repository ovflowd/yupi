using System;

namespace Yupi.Model.Domain
{
    public class MonsterPlantSeedBaseItem : FloorBaseItem
    {
        public override Item CreateNew()
        {
            return new MonsterPlantSeedItem()
            {
                BaseItem = this
            };
        }
    }
}