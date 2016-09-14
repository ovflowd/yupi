namespace Yupi.Model.Domain
{
    using System;

    public class PetGesture : Gesture
    {
        #region Fields

        public static readonly PetGesture Crazy = new PetGesture(5, "crz");
        public static readonly PetGesture Joy = new PetGesture(5, "sml");
        public static readonly PetGesture Puzzled = new PetGesture(5, "puz");
        public static readonly PetGesture Unknown1 = new PetGesture(5, "tng");
        public static readonly PetGesture Unknown2 = new PetGesture(5, "eyb");
        public static readonly PetGesture Unknown3 = new PetGesture(5, "mis");

        #endregion Fields

        #region Constructors

        protected PetGesture(int value, string displayName)
            : base(value, displayName)
        {
        }

        #endregion Constructors
    }
}