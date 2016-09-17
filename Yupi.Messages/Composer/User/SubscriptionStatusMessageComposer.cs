// ---------------------------------------------------------------------------------
// <copyright file="SubscriptionStatusMessageComposer.cs" company="https://github.com/sant0ro/Yupi">
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

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public class SubscriptionStatusMessageComposer : Yupi.Messages.Contracts.SubscriptionStatusMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, Subscription subscription)
        {
            // TODO refactor
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendString("club_habbo"); // product name

                if (subscription.HasLevel(ClubLevel.HC))
                {
                    int days = (subscription.ExpireTime - DateTime.Now).Days;
                    int activeFor = (DateTime.Now - subscription.ActivateTime).Days;
                    int months = days/31;

                    message.AppendInteger(days - months*31);
                    message.AppendInteger(1);
                    message.AppendInteger(months);
                    message.AppendInteger(1);
                    message.AppendBool(true);
                    message.AppendBool(true);
                    message.AppendInteger(activeFor);
                    message.AppendInteger(activeFor);
                    message.AppendInteger(10);
                }
                else
                {
                    message.AppendInteger(0);
                    message.AppendInteger(0);
                    message.AppendInteger(0);
                    message.AppendInteger(0);
                    message.AppendBool(false);
                    message.AppendBool(false);
                    message.AppendInteger(0);
                    message.AppendInteger(0);
                    message.AppendInteger(0);
                }
                session.Send(message);
            }
        }

        #endregion Methods
    }
}