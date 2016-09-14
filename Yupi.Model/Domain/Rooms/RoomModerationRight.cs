namespace Yupi.Model
{
    using System;

    using Headspring;

    public class RoomModerationRight : Enumeration<RoomModerationRight>
    {
        #region Fields

        public static readonly RoomModerationRight Everybody = new RoomModerationRight(2, "Everybody");
        public static readonly RoomModerationRight None = new RoomModerationRight(0, "None");
        public static readonly RoomModerationRight UsersWithRights = new RoomModerationRight(1, "UsersWithRights");

        #endregion Fields

        #region Constructors

        private RoomModerationRight(int value, string displayName)
            : base(value, displayName)
        {
        }

        #endregion Constructors
    }
}