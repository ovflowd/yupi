// ---------------------------------------------------------------------------------
// <copyright file="CopyLook.cs" company="https://github.com/sant0ro/Yupi">
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
using Yupi.Emulator.Data.Base.Adapters.Interfaces;
using Yupi.Emulator.Game.Commands.Interfaces;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Rooms;
using Yupi.Emulator.Game.Rooms.User;



namespace Yupi.Emulator.Game.Commands.Controllers
{
    /// <summary>
    ///     Class CopyLook. This class cannot be inherited.
    /// </summary>
     public sealed class CopyLook : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CopyLook" /> class.
        /// </summary>
        public CopyLook()
        {
            MinRank = -3;
            Description = "Copys a look of another user.";
            Usage = ":copy [USERNAME]";
            MinParams = 1;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            Room room = session.GetHabbo().CurrentRoom;

            RoomUser user = room.GetRoomUserManager().GetRoomUserByHabbo(pms[0]);
            if (user == null)
            {
                session.SendWhisper(Yupi.GetLanguage().GetVar("user_not_found"));
                return true;
            }

            string gender = user.GetClient().GetHabbo().Gender;
            string look = user.GetClient().GetHabbo().Look;
            session.GetHabbo().Gender = gender;
            session.GetHabbo().Look = look;
            using (IQueryAdapter adapter = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                adapter.SetQuery(
                    "UPDATE users SET gender = @gender, look = @look WHERE id = " + session.GetHabbo().Id);
                adapter.AddParameter("gender", gender);
                adapter.AddParameter("look", look);
                adapter.RunQuery();
            }

            RoomUser myUser = room.GetRoomUserManager().GetRoomUserByHabbo(session.GetHabbo().UserName);
            if (myUser == null) return true;

            SimpleServerMessageBuffer messageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("UpdateUserDataMessageComposer"));
            messageBuffer.AppendInteger(myUser.VirtualId);
            messageBuffer.AppendString(session.GetHabbo().Look);
            messageBuffer.AppendString(session.GetHabbo().Gender.ToLower());
            messageBuffer.AppendString(session.GetHabbo().Motto);
            messageBuffer.AppendInteger(session.GetHabbo().AchievementPoints);
            room.SendMessage(messageBuffer.GetReversedBytes());

            return true;
        }
    }
}