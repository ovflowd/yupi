// ---------------------------------------------------------------------------------
// <copyright file="SupportTicket.cs" company="https://github.com/sant0ro/Yupi">
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

    public class SupportTicket
    {
        #region Properties

        [Required]
        public virtual SupportCategory Category
        {
            get; set;
        }

        public virtual TicketCloseReason CloseReason
        {
            get; protected set;
        }

        [Required]
        public virtual DateTime CreatedAt {
            get; set;
        } = DateTime.Now;

        [Key]
        public virtual int Id
        {
            get; protected set;
        }

        [Required]
        public virtual string Message
        {
            get; set;
        }

        [ManyToMany]
        public virtual IList<ChatMessage> ReportedChats {
            get; set;
        } = new List<ChatMessage> ();

        public virtual UserInfo ReportedUser
        {
            get; set;
        }

        public virtual RoomData Room
        {
            get; set;
        }

        // TODO What???
        [Required]
        public virtual int Priority
        {
            get; set;
        }

        [Required]
        public virtual UserInfo Sender
        {
            get; set;
        }

        public virtual UserInfo Staff
        {
            get; protected set;
        }

        [Required]
        public virtual TicketStatus Status {
            get; protected set;
        } = TicketStatus.Open;

        // TODO Enum
        // type (3 or 4 for new style)
        public virtual int Type
        {
            get; set;
        }

        #endregion Properties

        #region Methods

        public virtual void Close(TicketCloseReason reason)
        {
            Status = TicketStatus.Closed;
            CloseReason = reason;
        }

        public virtual void Delete()
        {
            Status = TicketStatus.Closed;
            CloseReason = TicketCloseReason.Deleted;
        }

        public virtual void Pick(UserInfo moderator)
        {
            Status = TicketStatus.Picked;
            Staff = moderator;
        }

        public virtual void Release()
        {
            Status = TicketStatus.Open;
            Staff = null;
        }

        #endregion Methods
    }
}