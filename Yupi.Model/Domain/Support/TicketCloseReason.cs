using Headspring;

namespace Yupi.Model
{
    public class TicketCloseReason : Enumeration<TicketCloseReason>
    {
        public static readonly TicketCloseReason Resolved = new TicketCloseReason(0, "Resolved");
        public static readonly TicketCloseReason Invalid = new TicketCloseReason(1, "Invalid");
        public static readonly TicketCloseReason Abusive = new TicketCloseReason(2, "Abusive");
        public static readonly TicketCloseReason Deleted = new TicketCloseReason(3, "Deleted");

        protected TicketCloseReason(int value, string displayName) : base(value, displayName)
        {
        }
    }
}