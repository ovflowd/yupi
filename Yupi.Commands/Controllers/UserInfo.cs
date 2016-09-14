// ---------------------------------------------------------------------------------
// <copyright file="UserInfo.cs" company="https://github.com/sant0ro/Yupi">
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
/* copyright */
using System.Text;
using Yupi.Emulator.Game.Commands.Interfaces;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Users;

namespace Yupi.Emulator.Game.Commands.Controllers
{
    /// <summary>
    ///     Class UserInfo. This class cannot be inherited.
    /// </summary>
     public sealed class UserInfo : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="UserInfo" /> class.
        /// </summary>
        public UserInfo()
        {
            MinRank = 5;
            Description = "Tells you information of the typed username.";
            Usage = ":userinfo [USERNAME]";
            MinParams = 1;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            if (pms.Length != 1)
                return true;

            if (string.IsNullOrEmpty(pms[0]))
                return true;

            string userName = Yupi.FilterInjectionChars(pms[0]);

            Habbo userCached = Yupi.GetHabboForName(userName);

            if (userCached == null)
                return true;

            StringBuilder builder = new StringBuilder();

            if (userCached.CurrentRoom != null)
            {
                builder.Append($" - ROOM INFORMATION [{userCached.CurrentRoom.RoomId}] - \r");
                builder.Append($"Owner: {userCached.CurrentRoom.RoomData.Owner}\r");
                builder.Append($"Room Name: {userCached.CurrentRoom.RoomData.Name}\r");
                builder.Append($"Current Users: {userCached.CurrentRoom.UserCount} / {userCached.CurrentRoom.RoomData.UsersMax}");
            }

            session.SendNotif(string.Concat("User info for: ", userName, " \rUser ID: ", userCached.Id, ":\rRank: ",
                userCached.Rank, "\rCurrentTalentLevel: ", userCached.CurrentTalentLevel, " \rCurrent Room: ", userCached.CurrentRoomId,
                " \rCredits: ", userCached.Credits, "\rDuckets: ", userCached.Duckets, "\rDiamonds: ", userCached.Diamonds,
                "\rMuted: ", userCached.Muted.ToString(), "\r\r\r", builder.ToString()));

            return true;
        }
    }
}