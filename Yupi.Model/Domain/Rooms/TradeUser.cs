namespace Yupi.Model.Domain
{
    using System.Collections.Generic;

    [Ignore]
    public class TradeUser
    {
        #region Fields

        public List<UserItem> OfferedItems;
        public RoomEntity User;

        #endregion Fields

        #region Properties

        public bool HasAccepted
        {
            get; set;
        }

        #endregion Properties

        #region Constructors

        public TradeUser()
        {
            OfferedItems = new List<UserItem>();
        }

        #endregion Constructors
    }
}