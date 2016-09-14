namespace Yupi.Model.Domain
{
    using System;

    using Headspring;

    public class ChatSpeed : Enumeration<ChatSpeed>
    {
        #region Fields

        public static readonly ChatSpeed Fast = new ChatSpeed(0, "Fast");
        public static readonly ChatSpeed Normal = new ChatSpeed(1, "Normal");
        public static readonly ChatSpeed Slow = new ChatSpeed(2, "Slow");

        #endregion Fields

        #region Constructors

        private ChatSpeed(int value, string displayName)
            : base(value, displayName)
        {
        }

        #endregion Constructors
    }
}