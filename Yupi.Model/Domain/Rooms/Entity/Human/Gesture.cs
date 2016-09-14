using Headspring;

namespace Yupi.Model.Domain
{
    public class Gesture : Enumeration<Gesture>
    {
        public static readonly Gesture None = new Gesture(0, "");

        // TODO Can these be used by pets too?
        public static readonly Gesture Smile = new Gesture(1, "sml");
        public static readonly Gesture Angry = new Gesture(2, "agr");
        public static readonly Gesture Surprised = new Gesture(3, "srp");
        public static readonly Gesture Sad = new Gesture(4, "sad");

        protected Gesture(int value, string displayName) : base(value, displayName)
        {
        }
    }
}