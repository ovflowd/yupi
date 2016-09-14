namespace Yupi.Model.Domain
{
    using System;

    using Yupi.Model.Domain;

    [Ignore]
    public class BotEntity : HumanEntity
    {
        #region Constructors

        public BotEntity(Room room, int id)
            : base(room, id)
        {
        }

        #endregion Constructors

        #region Properties

        public override BaseInfo BaseInfo
        {
            get { return Info; }
        }

        public BotInfo Info
        {
            get; set;
        }

        public override EntityType Type
        {
            get { return EntityType.Bot; }
        }

        #endregion Properties
    }
}