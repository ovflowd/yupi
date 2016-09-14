using System.Collections.Generic;

namespace Yupi.Model.Domain
{
    [Ignore]
    public class TradeUser
    {
        public List<UserItem> OfferedItems;
        public RoomEntity User;

        public TradeUser()
        {
            OfferedItems = new List<UserItem>();
        }

        public bool HasAccepted { get; set; }
    }
}