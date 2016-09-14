using Headspring;

namespace Yupi.Model.Domain
{
    public class ChatBalloon : Enumeration<ChatBalloon>
    {
        public static readonly ChatBalloon Wide = new ChatBalloon(0, "Wide");
        public static readonly ChatBalloon Normal = new ChatBalloon(1, "Normal");
        public static readonly ChatBalloon Thin = new ChatBalloon(2, "Thin");

        private ChatBalloon(int value, string displayName) : base(value, displayName)
        {
        }
    }
}