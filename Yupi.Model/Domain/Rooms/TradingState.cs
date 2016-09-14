namespace Yupi.Model
{
    using System;

    using Headspring;

    public class TradingState : Enumeration<TradingState>
    {
        #region Fields

        public static readonly TradingState Allowed = new TradingState(3, "Allowed");
        public static readonly TradingState NotAllowed = new TradingState(0, "NotAllowed");
        public static readonly TradingState UsersWithRights = new TradingState(1, "UsersWithRights");

        #endregion Fields

        #region Constructors

        private TradingState(int value, string displayName)
            : base(value, displayName)
        {
        }

        #endregion Constructors
    }
}