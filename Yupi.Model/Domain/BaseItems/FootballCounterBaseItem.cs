namespace Yupi.Model.Domain
{
    using System;

    public class FootballCounterBaseItem : FloorBaseItem
    {
        #region Properties

        // TODO Should be enum
        public virtual int Color
        {
            get; set;
        }

        #endregion Properties
    }
}