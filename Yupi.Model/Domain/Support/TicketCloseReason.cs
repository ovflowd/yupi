namespace Yupi.Model
{
    using System;

    using Headspring;

    public class TicketCloseReason : Enumeration<TicketCloseReason>
    {
        #region Fields

        public static readonly TicketCloseReason Abusive = new TicketCloseReason(2, "Abusive");
        public static readonly TicketCloseReason Deleted = new TicketCloseReason(3, "Deleted");
        public static readonly TicketCloseReason Invalid = new TicketCloseReason(1, "Invalid");
        public static readonly TicketCloseReason Resolved = new TicketCloseReason(0, "Resolved");

        #endregion Fields

        #region Constructors

        protected TicketCloseReason(int value, string displayName)
            : base(value, displayName)
        {
        }

        #endregion Constructors
    }
}