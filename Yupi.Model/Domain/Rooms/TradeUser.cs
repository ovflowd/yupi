using System.Collections.Generic;

namespace Yupi.Model.Domain
{
    [Ignore]
    public class TradeUser
    {
        public List<UserItem> OfferedItems;
        public RoomEntity User;
        public bool HasAccepted { get; set; }

        public TradeUser()
        {
            OfferedItems = new List<UserItem>();
        }
    }
}