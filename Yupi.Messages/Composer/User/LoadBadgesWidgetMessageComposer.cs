// ---------------------------------------------------------------------------------
// <copyright file="LoadBadgesWidgetMessageComposer.cs" company="https://github.com/sant0ro/Yupi">
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
namespace Yupi.Messages.User
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Yupi.Model.Domain;
    using Yupi.Model.Domain.Components;
    using Yupi.Protocol.Buffers;

    public class LoadBadgesWidgetMessageComposer : Contracts.LoadBadgesWidgetMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, UserBadgeComponent badges)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(badges.Badges.Count);

                foreach (UserBadge badge in badges.Badges)
                {
                    message.AppendInteger(1); // TODO Magic constant
                    message.AppendString(badge.Badge.Code);
                }

                IList<UserBadge> visibleBadges = badges.GetVisible();

                message.AppendInteger(visibleBadges.Count);

                foreach (UserBadge badge in visibleBadges)
                {
                    message.AppendInteger(badge.Slot);
                    message.AppendString(badge.Badge.Code);
                }

                session.Send(message);
            }
        }

        #endregion Methods
    }
}