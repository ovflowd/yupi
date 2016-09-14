namespace Yupi.Model.Domain
{
    using System;

    public class BanzaiScoreBaseItem : FloorBaseItem
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