// ---------------------------------------------------------------------------------
// <copyright file="ModerationToolIssueMessageComposer.cs" company="https://github.com/sant0ro/Yupi">
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

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public class ModerationToolIssueMessageComposer : Yupi.Messages.Contracts.ModerationToolIssueMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, SupportTicket ticket)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(ticket.Id);
                message.AppendInteger((int) ticket.Status);
                message.AppendInteger(ticket.Type);
                message.AppendInteger(ticket.Category);
                message.AppendInteger((int) (DateTime.Now - ticket.CreatedAt).TotalMilliseconds);
                message.AppendInteger(ticket.Score);
                message.AppendInteger(1); // TODO magic constant
                message.AppendInteger(ticket.Sender.Id);
                message.AppendString(ticket.Sender.Name);
                message.AppendInteger(ticket.ReportedUser.Id);
                message.AppendString(ticket.ReportedUser.Name);
                message.AppendInteger(ticket.Staff.Id);
                message.AppendString(ticket.Staff.Name);
                message.AppendString(ticket.Message);
                message.AppendInteger(0);

                message.AppendInteger(ticket.ReportedChats.Count);

                foreach (string str in ticket.ReportedChats)
                {
                    message.AppendString(str);
                    message.AppendInteger(-1);
                    message.AppendInteger(-1);
                }

                session.Send(message);
            }
        }

        #endregion Methods
    }
}