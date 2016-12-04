// ---------------------------------------------------------------------------------
// <copyright file="ModerationToolIssueChatlogMessageComposer.cs" company="https://github.com/sant0ro/Yupi">
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
namespace Yupi.Messages.Support
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public class ModerationToolIssueChatlogMessageComposer : Yupi.Messages.Contracts.ModerationToolIssueChatlogMessageComposer
    {
        #region Methods

        // TODO Refactor
        // TODO Room might be null
        public override void Compose(Yupi.Protocol.ISender session, SupportTicket ticket)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(ticket.Id);
                message.AppendInteger(ticket.Sender.Id);
                message.AppendInteger(ticket.ReportedUser.Id);
                message.AppendInteger(ticket.Room.Id);

                // TODO Hardcoded message
                message.AppendByte(1);
                message.AppendShort(2);
                message.AppendString("roomName");
                message.AppendByte(2);
                message.AppendString(ticket.Room.Name);
                message.AppendString("roomId");
                message.AppendByte(1);
                message.AppendInteger(ticket.Room.Id);

                int count = Math.Min(ticket.Room.Chatlog.Count, 60);
                message.AppendShort((short) count);

                for (int i = 1; i <= count; ++i)
                {
                    ChatMessage entry = ticket.Room.Chatlog[ticket.Room.Chatlog.Count - i];
                    message.AppendInteger((int) (DateTime.Now - entry.Timestamp).TotalMilliseconds);
                    message.AppendInteger(entry.User.Id);
                    message.AppendString(entry.User.Name);
                    message.AppendString(entry.OriginalMessage);
                    message.AppendBool(!entry.Whisper);
                }

                session.Send(message);
            }
        }

        #endregion Methods
    }
}