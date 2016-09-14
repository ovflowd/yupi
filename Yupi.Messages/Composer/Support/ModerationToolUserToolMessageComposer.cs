// ---------------------------------------------------------------------------------
// <copyright file="ModerationToolUserToolMessageComposer.cs" company="https://github.com/sant0ro/Yupi">
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
    using System.Data;
    using System.Linq;

    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public class ModerationToolUserToolMessageComposer : Yupi.Messages.Contracts.ModerationToolUserToolMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, UserInfo user)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                // TODO Refactor
                message.AppendInteger(user.Id);
                message.AppendString(user.Name);
                message.AppendString(user.Look);
                message.AppendInteger((int) (DateTime.Now - user.CreateDate).TotalHours);
                message.AppendInteger((int) (DateTime.Now - user.LastOnline).TotalHours);
                // TODO Hardcoded value
                message.AppendBool(true);
                message.AppendInteger(user.SupportTickets.Count);
                message.AppendInteger(
                    user.SupportTickets.Where(
                            x => x.Status == TicketStatus.Closed && x.CloseReason == Yupi.Model.TicketCloseReason.Abusive)
                        .Count());
                message.AppendInteger(user.Cautions.Count);
                message.AppendInteger(user.Bans.Count);

                message.AppendInteger(0);

                TradeLock tradeLock = user.TradeLocks.Last(x => !x.HasExpired());

                message.AppendString(tradeLock != null ? tradeLock.ExpiresAt.ToString() : "Not trade-locked");

                message.AppendString(user.HasPermission("view_ip") ? user.LastIp.ToString() : string.Empty);
                message.AppendInteger(user.Id);
                message.AppendInteger(0);

                // TODO Should these really be displayed here?
                message.AppendString("E-Mail:         " + user.Email);
                message.AppendString("Rank ID:        " + user.Rank);

                session.Send(message);
            }
        }

        #endregion Methods
    }
}