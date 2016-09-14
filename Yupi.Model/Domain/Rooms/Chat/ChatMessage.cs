// ---------------------------------------------------------------------------------
// <copyright file="ChatMessage.cs" company="https://github.com/sant0ro/Yupi">
//   Copyright (c) 2016 Claudio Santoro, TheDoctor
// </copyright>
// <license>
//   Permission is hereby granted, free of charge, to any person obtaining a copy
//   of this software and associated documentation files (the "Software"), to deal
//   in the Software without restriction, including without limitation the rights
//   to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//   copies of the Software, and to permit persons to whom the Software is
//   furnished to do so, subject to the following conditions:
//
//   The above copyright notice and this permission notice shall be included in
//   all copies or substantial portions of the Software.
//
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//   IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//   FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//   AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//   LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//   OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//   THE SOFTWARE.
// </license>
// ---------------------------------------------------------------------------------
namespace Yupi.Model.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Yupi.Model.Domain;

    public class ChatMessage
    {
        #region Fields

        private static Dictionary<string, Gesture> Emotions;

        #endregion Fields

        #region Properties

        public virtual ChatBubbleStyle Bubble
        {
            get; set;
        }

        [Ignore]
        public virtual RoomEntity Entity
        {
            get; set;
        }

        public virtual int Id
        {
            get; set;
        }

        public virtual IList<Link> Links
        {
            get; protected set;
        }

        public virtual string Message
        {
            get; protected set;
        }

        public virtual DateTime Timestamp
        {
            get; protected set;
        }

        public virtual UserInfo User
        {
            get; set;
        }

        public virtual bool Whisper
        {
            get; set;
        }

        #endregion Properties

        #region Constructors

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

        public ChatMessage(string message)
            : this()
        {
            Message = message;
        }

        protected ChatMessage()
        {
            Links = new List<Link>();
            Bubble = ChatBubbleStyle.Normal;
            Timestamp = DateTime.Now;
        }

        #endregion Constructors

        #region Methods

        public virtual string FilteredMessage()
        {
            // TODO Filter
            return Message.Trim();
        }

        public virtual Gesture GetEmotion()
        {
            // TODO Cache
            // Default is Gesture.None (because it has the value 0)
            return Emotions.FirstOrDefault(x => Message.Contains(x.Key)).Value ?? Gesture.None;
        }

        #endregion Methods
    }
}