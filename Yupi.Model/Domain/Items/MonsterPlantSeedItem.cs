namespace Yupi.Model.Domain
{
    using System;

    public class MonsterPlantSeedItem : FloorItem<MonsterPlantSeedBaseItem>
    {
        #region Fields

        [Ignore]
        private static Random Rand = new Random();

        #endregion Fields

        #region Properties

        // TODO Is this correct?
        public virtual int Race
        {
            get; set;
        }

        #endregion Properties

        #region Methods

        public override string GetExtraData()
        {
            return Race.ToString();
        }

        public override void TryParseExtraData(string data)
        {
            Race = Rand.Next(12);
        }

        #endregion Methods
    }
}