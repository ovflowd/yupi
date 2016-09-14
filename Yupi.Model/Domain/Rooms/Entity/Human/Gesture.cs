namespace Yupi.Model.Domain
{
    using System;

    using Headspring;

    public class Gesture : Enumeration<Gesture>
    {
        #region Fields

        public static readonly Gesture Angry = new Gesture(2, "agr");
        public static readonly Gesture None = new Gesture(0, "");
        public static readonly Gesture Sad = new Gesture(4, "sad");

        // TODO Can these be used by pets too?
        public static readonly Gesture Smile = new Gesture(1, "sml");
        public static readonly Gesture Surprised = new Gesture(3, "srp");

        #endregion Fields

        #region Constructors

        protected Gesture(int value, string displayName)
            : base(value, displayName)
        {
        }

        #endregion Constructors
    }
}