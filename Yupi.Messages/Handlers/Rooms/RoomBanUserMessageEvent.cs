// ---------------------------------------------------------------------------------
// <copyright file="RoomBanUserMessageEvent.cs" company="https://github.com/sant0ro/Yupi">
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
namespace Yupi.Messages.Rooms
{
    using System;

    public class RoomBanUserMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            /*
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(session.GetHabbo().CurrentRoomId);

            if (room == null || (room.RoomData.WhoCanBan == 0 && !room.CheckRights(session, true)) ||
                (room.RoomData.WhoCanBan == 1 && !room.CheckRights(session)))
                return;

            uint userId = request.GetUInt32();

            // TODO unused
            request.GetUInt32();

            string text = request.GetString();

            RoomUser roomUserByHabbo = room.GetRoomUserManager().GetRoomUserByHabbo(userId);

            if (roomUserByHabbo == null || roomUserByHabbo.IsBot)
                return;

            if (roomUserByHabbo.GetClient().UserData.Info.HasPermission("fuse_mod") ||
                roomUserByHabbo.GetClient().UserData.Info.HasPermission("fuse_no_kick")) // TODO Tell user about this behaviour (Whisper)
                return;

            long time = 0L;
            // TODO improve ban length parsing
            if (text.ToLower().Contains("hour"))
                time = 3600L;
            else if (text.ToLower().Contains("day"))
                time = 86400L;
            else if (text.ToLower().Contains("perm"))
                time = 788922000L;

            room.AddBan(userId, time);
            room.GetRoomUserManager().RemoveUserFromRoom(roomUserByHabbo.GetClient(), true, true);
            session.CurrentRoomUserId = -1;
            */
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}