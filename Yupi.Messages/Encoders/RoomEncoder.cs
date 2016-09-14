// ---------------------------------------------------------------------------------
// <copyright file="RoomEncoder.cs" company="https://github.com/sant0ro/Yupi">
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
namespace Yupi.Messages.Encoders
{
    using System;

    using Yupi.Controller;
    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Domain.Components;
    using Yupi.Protocol.Buffers;

    public static class RoomEncoder
    {
        #region Methods

        public static void Append(this ServerMessage message, RoomData data)
        {
            RoomManager manager = DependencyFactory.Resolve<RoomManager>();
            Room room = manager.GetIfLoaded(data);
            message.AppendInteger(data.Id);
            message.AppendString(data.Name);
            message.AppendInteger(data.Owner.Id);
            message.AppendString(data.Owner.Name);
            message.AppendInteger(data.State.Value);
            message.AppendInteger(room == null ? 0 : room.GetUserCount());
            message.AppendInteger(data.UsersMax);
            message.AppendString(data.Description);
            message.AppendInteger(data.TradeState.Value);
            message.AppendInteger(data.Score); // Score
            message.AppendInteger(data.Score); // Ranking Difference?
            message.AppendInteger(data.Category.Id);
            message.AppendInteger(data.Tags.Count);

            foreach (string tag in data.Tags)
            {
                message.AppendString(tag);
            }

            RoomFlags flags = data.GetFlags();

            message.AppendInteger((int) flags);

            if ((flags & RoomFlags.Image) > 0)
            {
                message.AppendString(data.NavigatorImage);
            }

            if ((flags & RoomFlags.Group) > 0)
            {
                message.AppendInteger(data.Group.Id);
                message.AppendString(data.Group.Name);
                message.AppendString(data.Group.Badge);
            }

            if ((flags & RoomFlags.Event) > 0)
            {
                message.AppendString(data.Event.Name);
                message.AppendString(data.Event.Description);
                message.AppendInteger((int) (data.Event.ExpiresAt - DateTime.Now).TotalMinutes);
            }
        }

        public static void Append(this ServerMessage message, RoomChatSettings chat)
        {
            message.AppendInteger(chat.Type.Value);
            message.AppendInteger(chat.Balloon.Value);
            message.AppendInteger(chat.Speed.Value);
            message.AppendInteger(chat.MaxDistance);
            message.AppendInteger(chat.FloodProtection.Value);
        }

        public static void Append(this ServerMessage message, ModerationSettings modSettings)
        {
            message.AppendInteger(modSettings.WhoCanMute.Value);
            message.AppendInteger(modSettings.WhoCanKick.Value);
            message.AppendInteger(modSettings.WhoCanBan.Value);
        }

        #endregion Methods
    }
}