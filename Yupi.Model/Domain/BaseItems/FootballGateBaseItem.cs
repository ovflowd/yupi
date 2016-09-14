namespace Yupi.Model.Domain
{
    using System;

    public class FootballGateBaseItem : FloorBaseItem
    {
        #region Properties

        // TODO Should be enum
        public virtual int Color
        {
            get; set;
        }

        #endregion Properties

        #region Methods

        public override Item CreateNew()
        {
            return new FootballGateItem()
            {
                BaseItem = this
            };
        }

        #endregion Methods
    }
}