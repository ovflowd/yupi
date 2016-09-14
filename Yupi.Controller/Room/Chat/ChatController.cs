// ---------------------------------------------------------------------------------
// <copyright file="ChatController.cs" company="https://github.com/sant0ro/Yupi">
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
namespace Yupi.Controller
{
    using System;

    using Yupi.Messages.Contracts;
    using Yupi.Model;
    using Yupi.Model.Domain;

    public class ChatController
    {
        #region Fields

        public const int MAX_MESSAGE_LENGTH = 100;

        #endregion Fields

        #region Methods

        public void Chat(Habbo session, string message, ChatBubbleStyle bubble, int count = -1)
        {
            Chat(session, message, bubble, (user, entry) =>
            {
                user.Router.GetComposer<ChatMessageComposer>()
                    .Compose(user, entry, count);
            });
        }

        public void Shout(Habbo session, string message, ChatBubbleStyle bubble, int count = -1)
        {
            Chat(session, message, bubble, (user, entry) =>
            {
                user.Router.GetComposer<ShoutMessageComposer>()
                    .Compose(user, entry, count);
            });
        }

        public void Whisper(Habbo session, string message, ChatBubbleStyle bubble, RoomEntity target, int count = -1)
        {
            if (!Validate(ref message) || TryHandleCommand(message) || !bubble.CanUse(session.Info))
            {
                return;
            }

            session.RoomEntity.Wake();

            ChatMessage entry = CreateMessage(session, message, bubble);
            entry.Whisper = true;
            // TODO Save Whisper target
            // TODO Save Chatlog

            session.Router.GetComposer<WhisperMessageComposer>().Compose(session, entry, count);

            if (target != null)
            {
                target.HandleChatMessage(session.RoomEntity, targetSession =>
                {
                    targetSession.Router.GetComposer<WhisperMessageComposer>()
                        .Compose(targetSession, entry, count);
                });
            }

            // TODO Trigger Wired
        }

        private void Chat(Habbo session, string message, ChatBubbleStyle bubble, Action<Habbo, ChatMessage> composer)
        {
            if (!Validate(ref message) || TryHandleCommand(message) || !bubble.CanUse(session.Info))
            {
                return;
            }

            session.RoomEntity.Wake();

            ChatMessage entry = CreateMessage(session, message, bubble);

            session.Info.Preferences.ChatBubbleStyle = bubble;
            // TODO Save Preferences
            // TODO Save Chatlog
            session.Room.EachEntity(
                entity => { entity.HandleChatMessage(session.RoomEntity, user => composer(user, entry)); }
            );

            // TODO Trigger Wired
        }

        private ChatMessage CreateMessage(Habbo session, string message, ChatBubbleStyle bubble)
        {
            ChatMessage msg = new ChatMessage(message)
            {
                Entity = session.RoomEntity,
                Bubble = bubble,
                User = session.Info
            };

            session.Room.Data.Chatlog.Add(msg);
            return msg;
        }

        private bool TryHandleCommand(string message)
        {
            /* TODO Command manager
                 * return msg.StartsWith(":") && CommandsManager.TryExecute(msg.Substring(1), session)
                */
            return false;
        }

        private bool Validate(ref string message)
        {
            if (message.Length > MAX_MESSAGE_LENGTH)
            {
                return false;
            }

            /* TODO Implement
                if (!ServerSecurityChatFilter.CanTalk(session, msg))
                    return false;
                    */

            // TODO Wordfilter
            // TODO Flood
            // TODO Room Mute

            return true;
        }

        #endregion Methods
    }
}