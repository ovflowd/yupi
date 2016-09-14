// ---------------------------------------------------------------------------------
// <copyright file="LoadModerationToolMessageComposer.cs" company="https://github.com/sant0ro/Yupi">
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

    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public class LoadModerationToolMessageComposer : Yupi.Messages.Contracts.LoadModerationToolMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, IList<SupportTicket> Tickets,
            IList<ModerationTemplate> Templates, IList<string> UserMessagePresets, IList<string> RoomMessagePresets,
            UserInfo user)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(Tickets.Count);

                foreach (SupportTicket current in Tickets)
                {
                    //SerializeTicket(message);
                    throw new NotImplementedException();
                }

                message.AppendInteger(UserMessagePresets.Count);

                foreach (string text in UserMessagePresets)
                {
                    message.AppendString(text);
                }

                // TODO Implement categories correctly... This is a mess...
                IEnumerable<ModerationTemplate> enumerable =
                    (from x in Templates where x.Category == -1 select x).ToArray();

                message.AppendInteger(enumerable.Count());
                using (IEnumerator<ModerationTemplate> enumerator3 = enumerable.GetEnumerator())
                {
                    bool first = true;

                    while (enumerator3.MoveNext())
                    {
                        ModerationTemplate template = enumerator3.Current;
                        IEnumerable<ModerationTemplate> enumerable2 =
                            (from x in Templates where x.Category == (long) (ulong) template.Id select x)
                                .ToArray();
                        message.AppendString(template.CName);
                        message.AppendBool(first);
                        message.AppendInteger(enumerable2.Count());

                        foreach (ModerationTemplate current3 in enumerable2)
                        {
                            message.AppendString(current3.Caption);
                            message.AppendString(current3.BanMessage);
                            message.AppendInteger(current3.BanHours);
                            message.AppendInteger(current3.AvatarBan);
                            message.AppendInteger(current3.Mute);
                            message.AppendInteger(current3.TradeLock);
                            message.AppendString(current3.WarningMessage);
                            message.AppendBool(true);
                        }

                        first = false;
                    }
                }

                // TODO Hardcoded
                message.AppendBool(true); //ticket_queue_button
                message.AppendBool(user.HasPermission("fuse_chatlogs")); //chatlog_button
                message.AppendBool(user.HasPermission("fuse_alert")); //message_button
                message.AppendBool(true); //modaction_but
                message.AppendBool(user.HasPermission("fuse_ban")); //ban_button
                message.AppendBool(true);
                message.AppendBool(user.HasPermission("fuse_kick")); //kick_button

                message.AppendInteger(RoomMessagePresets.Count);

                foreach (string current4 in RoomMessagePresets)
                    message.AppendString(current4);

                session.Send(message);
            }
        }

        private void SerializeTicket(ServerMessage message, SupportTicket ticket)
        {
            message.AppendInteger(ticket.Id);
            message.AppendInteger((int) ticket.Status);
            message.AppendInteger(ticket.Type);
            message.AppendInteger(ticket.Category);
            message.AppendInteger((int) (DateTime.Now - ticket.CreatedAt).TotalMilliseconds);
            message.AppendInteger(ticket.Score);
            message.AppendInteger(1);
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
        }

        #endregion Methods
    }
}