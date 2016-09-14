namespace Yupi.Model
{
    using System;

    using Headspring;

    /// <summary>
    /// Room right level
    /// </summary>
    public class RoomRightLevel : Enumeration<RoomRightLevel>, IStatusString
    {
        #region Fields

        /// <summary>
        /// Rights as group admin
        /// </summary>
        public static readonly RoomRightLevel Group_Admin = new RoomRightLevel(3, "Group_Admin");

        /// <summary>
        /// Rights through group
        /// </summary>
        public static readonly RoomRightLevel Group_Rights = new RoomRightLevel(2, "Group_Rights");

        /// <summary>
        /// Moderator rights
        /// </summary>
        public static readonly RoomRightLevel Moderator = new RoomRightLevel(5, "Moderator");

        /// <summary>
        /// No Rights
        /// </summary>
        public static readonly RoomRightLevel None = new RoomRightLevel(0, "None");

        /// <summary>
        /// Room Owner rights
        /// </summary>
        public static readonly RoomRightLevel Owner = new RoomRightLevel(4, "Owner");

        /// <summary>
        /// Rights given by room owner
        /// </summary>
        public static readonly RoomRightLevel Rights = new RoomRightLevel(1, "Rights");

        #endregion Fields

        #region Constructors

        protected RoomRightLevel(int value, string displayName)
            : base(value, displayName)
        {
        }

        #endregion Constructors

        #region Methods

        public string ToStatusString()
        {
            return "flatctrl " + Value;
        }

        #endregion Methods
    }
}