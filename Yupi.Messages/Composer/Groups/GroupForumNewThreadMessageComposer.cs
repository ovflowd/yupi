﻿// ---------------------------------------------------------------------------------
// <copyright file="GroupForumNewThreadMessageComposer.cs" company="https://github.com/sant0ro/Yupi">
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
namespace Yupi.Messages.Groups
{
    using System;

    using Yupi.Protocol.Buffers;

    public class GroupForumNewThreadMessageComposer : Yupi.Messages.Contracts.GroupForumNewThreadMessageComposer
    {
        #region Methods

        // TODO Hardcoded
        public override void Compose(Yupi.Protocol.ISender session, int groupId, int threadId, int habboId,
            string subject, string content, int timestamp)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                throw new NotImplementedException();
                /*
                message.AppendInteger(groupId);
                message.AppendInteger(threadId);
                message.AppendInteger(habboId);
                message.AppendString(subject);
                message.AppendString(content);
                message.AppendBool(false);
                message.AppendBool(false);
                message.AppendInteger(Yupi.GetUnixTimeStamp() - timestamp);
                message.AppendInteger(1);
                message.AppendInteger(0);
                message.AppendInteger(0);
                message.AppendInteger(1);
                message.AppendString(string.Empty);
                message.AppendInteger(Yupi.GetUnixTimeStamp() - timestamp);
                message.AppendByte(1);
                message.AppendInteger(1);
                message.AppendString(string.Empty);
                message.AppendInteger(42);
                session.Send (message);
                */
            }
        }

        #endregion Methods
    }
}