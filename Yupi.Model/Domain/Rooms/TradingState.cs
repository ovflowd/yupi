using Headspring;

namespace Yupi.Model
{
    public class TradingState : Enumeration<TradingState>
    {
        public static readonly TradingState NotAllowed = new TradingState(0, "NotAllowed");
        public static readonly TradingState UsersWithRights = new TradingState(1, "UsersWithRights");
        public static readonly TradingState Allowed = new TradingState(3, "Allowed");

        private TradingState(int value, string displayName) : base(value, displayName)
        {
        }
    }
}