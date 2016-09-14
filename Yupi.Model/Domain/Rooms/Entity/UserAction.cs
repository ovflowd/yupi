namespace Yupi.Model.Domain
{
    using System;

    using Headspring;

    public class UserAction : Enumeration<UserAction>
    {
        #region Fields

        public static readonly UserAction Blow = new UserAction(2, "Blow");
        public static readonly UserAction Idle = new UserAction(5, "Idle");
        public static readonly UserAction Jump = new UserAction(6, "Jump");
        public static readonly UserAction Laugh = new UserAction(3, "Laugh");
        public static readonly UserAction None = new UserAction(0, "None");
        public static readonly UserAction Respect = new UserAction(7, "Respect");

        // TODO What is this? (Maybe unused/legacy)
        public static readonly UserAction Unknown = new UserAction(4, "Unknown");
        public static readonly UserAction Wave = new UserAction(1, "Wave");

        #endregion Fields

        #region Constructors

        protected UserAction(int value, string displayName)
            : base(value, displayName)
        {
        }

        #endregion Constructors
    }
}