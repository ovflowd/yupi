using System;

namespace Yupi.Model.Domain
{
    public class MonsterPlantSeedItem : FloorItem<MonsterPlantSeedBaseItem>
    {
        [Ignore] private static readonly Random Rand = new Random();

        // TODO Is this correct?
        public virtual int Race { get; set; }

        public override void TryParseExtraData(string data)
        {
            Race = Rand.Next(12);
        }

        public override string GetExtraData()
        {
            return Race.ToString();
        }
    }
}