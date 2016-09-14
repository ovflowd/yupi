namespace Yupi.Model.Domain
{
    using System;
    using System.Collections.Generic;

    public class VendingBaseItem : FloorBaseItem
    {
        #region Properties

        public virtual IList<int> VendingIds
        {
            get; protected set;
        }

        #endregion Properties

        #region Constructors

        public VendingBaseItem()
        {
            VendingIds = new List<int>();
        }

        #endregion Constructors
    }
}