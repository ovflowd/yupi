using System;
using System.Collections.Generic;

namespace Yupi.Model.Domain
{
    public class VendingBaseItem : FloorBaseItem
    {
        public virtual IList<int> VendingIds { get; protected set; }

        public VendingBaseItem()
        {
            VendingIds = new List<int>();
        }
    }
}