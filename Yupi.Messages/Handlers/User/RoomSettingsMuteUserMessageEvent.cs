// ---------------------------------------------------------------------------------
// <copyright file="RoomSettingsMuteUserMessageEvent.cs" company="https://github.com/sant0ro/Yupi">
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

    using Yupi.Messages.Contracts;
    using Yupi.Model.Domain;
    using Yupi.Util;

    public class RoomSettingsMuteUserMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message,
            Yupi.Protocol.IRouter router)
        {
            int targetUserId = message.GetInteger();

            message.GetUInt32(); // TODO Unknown

            int duration = message.GetInteger();

            Room room = session.Room;

            if (room == null
                || !room.Data.ModerationSettings.CanMute(session.Info))
            {
                return;
            }

            UserEntity targetUser = room.GetEntity(targetUserId) as UserEntity;

            if (targetUser == null || targetUser.UserInfo.Rank >= session.Info.Rank)
                return;

            room.Data.MutedEntities.Add(new RoomMute()
            {
                Entity = targetUser.UserInfo,
                ExpiresAt = DateTime.Now.AddMinutes(duration)
            });

            targetUser.User.Router.GetComposer<SuperNotificationMessageComposer>()
                .Compose(targetUser.User, T._("Notice"),
                    string.Format(T._("The owner of the room has muted you for {0} minutes!"), duration), "", "", "", 4);
        }

        #endregion Methods
    }
}