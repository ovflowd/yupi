namespace Yupi.Model.Domain
{
    using System;

    using Headspring;

    public class ChatBalloon : Enumeration<ChatBalloon>
    {
        #region Fields

        public static readonly ChatBalloon Normal = new ChatBalloon(1, "Normal");
        public static readonly ChatBalloon Thin = new ChatBalloon(2, "Thin");
        public static readonly ChatBalloon Wide = new ChatBalloon(0, "Wide");

        #endregion Fields

        #region Constructors

        private ChatBalloon(int value, string displayName)
            : base(value, displayName)
        {
        }

        #endregion Constructors
    }
}