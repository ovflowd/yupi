namespace Yupi.Model.Domain
{
    public class PetGesture : Gesture
    {
        public static readonly PetGesture Joy = new PetGesture(5, "sml");
        public static readonly PetGesture Crazy = new PetGesture(5, "crz");
        public static readonly PetGesture Unknown1 = new PetGesture(5, "tng");
        public static readonly PetGesture Unknown2 = new PetGesture(5, "eyb");
        public static readonly PetGesture Unknown3 = new PetGesture(5, "mis");
        public static readonly PetGesture Puzzled = new PetGesture(5, "puz");

        protected PetGesture(int value, string displayName) : base(value, displayName)
        {
        }
    }
}