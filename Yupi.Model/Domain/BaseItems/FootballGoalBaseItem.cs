namespace Yupi.Model.Domain
{
    using System;

    public class FootballGoalBaseItem : FloorBaseItem
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