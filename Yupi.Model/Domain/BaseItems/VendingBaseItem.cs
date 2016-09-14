using System.Collections.Generic;

namespace Yupi.Model.Domain
{
    public class VendingBaseItem : FloorBaseItem
    {
        public VendingBaseItem()
        {
            VendingIds = new List<int>();
        }

        public virtual IList<int> VendingIds { get; protected set; }
    }
}