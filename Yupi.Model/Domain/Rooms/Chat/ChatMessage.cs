using System;
using Yupi.Model.Domain;
using System.Collections.Generic;
using System.Linq;

namespace Yupi.Model.Domain
{
    public class ChatMessage
    {
        public virtual int Id { get; set; }

        [Ignore]
        public virtual RoomEntity Entity { get; set; }

        public virtual DateTime Timestamp { get; protected set; }
        public virtual string Message { get; protected set; }
        public virtual ChatBubbleStyle Bubble { get; set; }
        public virtual IList<Link> Links { get; protected set; }
        public virtual bool Whisper { get; set; }
        public virtual UserInfo User { get; set; }

        private static Dictionary<string, Gesture> Emotions;

        static ChatMessage()
        {
            Emotions = new Dictionary<string, Gesture>
            {
                // Smile
                {":)", Gesture.Smile},
                {";)", Gesture.Smile},
                {":d", Gesture.Smile},
                {";d", Gesture.Smile},
                {":]", Gesture.Smile},
                {";]", Gesture.Smile},
                {"=)", Gesture.Smile},
                {"=]", Gesture.Smile},
                {":-)", Gesture.Smile},

                // Angry
                {">:(", Gesture.Angry},
                {">:[", Gesture.Angry},
                {">;[", Gesture.Angry},
                {">;(", Gesture.Angry},
                {">=(", Gesture.Angry},
                {":@", Gesture.Angry},

                // Surprised
                {":o", Gesture.Surprised},
                {";o", Gesture.Surprised},
                {">;o", Gesture.Surprised},
                {">:o", Gesture.Surprised},
                {"=o", Gesture.Surprised},
                {">=o", Gesture.Surprised},

                // Sad
                {";'(", Gesture.Sad},
                {";[", Gesture.Sad},
                {":[", Gesture.Sad},
                {";(", Gesture.Sad},
                {"=(", Gesture.Sad},
                {"='(", Gesture.Sad},
                {"=[", Gesture.Sad},
                {"='[", Gesture.Sad},
                {":(", Gesture.Sad},
                {":-(", Gesture.Sad}
            };
        }

        public ChatMessage(string message) : this()
        {
            Message = message;
        }

        protected ChatMessage()
        {
            Links = new List<Link>();
            Bubble = ChatBubbleStyle.Normal;
            Timestamp = DateTime.Now;
        }

        public virtual Gesture GetEmotion()
        {
            // TODO Cache
            // Default is Gesture.None (because it has the value 0)
            return Emotions.FirstOrDefault(x => Message.Contains(x.Key)).Value ?? Gesture.None;
        }

        public virtual string FilteredMessage()
        {
            // TODO Filter
            return Message.Trim();
        }
    }
}