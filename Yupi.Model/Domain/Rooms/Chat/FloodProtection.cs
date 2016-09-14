namespace Yupi.Model.Domain
{
    using System;

    using Headspring;

    public class FloodProtection : Enumeration<FloodProtection>
    {
        #region Fields

        public static readonly FloodProtection Extra = new FloodProtection(0, "Extra");
        public static readonly FloodProtection Minimal = new FloodProtection(2, "Minimal");
        public static readonly FloodProtection Standard = new FloodProtection(1, "Standard");

        #endregion Fields

        #region Constructors

        private FloodProtection(int value, string displayName)
            : base(value, displayName)
        {
        }

        #endregion Constructors
    }
}