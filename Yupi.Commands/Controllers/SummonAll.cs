// ---------------------------------------------------------------------------------
// <copyright file="SummonAll.cs" company="https://github.com/sant0ro/Yupi">
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
using Yupi.Emulator.Game.Commands.Interfaces;
using Yupi.Emulator.Game.GameClients.Interfaces;

namespace Yupi.Emulator.Game.Commands.Controllers
{
    /// <summary>
    ///     Class SummonAll. This class cannot be inherited.
    /// </summary>
     public sealed class SummonAll : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="SummonAll" /> class.
        /// </summary>
        public SummonAll()
        {
            MinRank = 7;
            Description = "Summon all users online to the room you are in.";
            Usage = ":summonall [reason]";
            MinParams = -1;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            string reason = string.Join(" ", pms);

			Router.GetComposer<SuperNotificationMessageComposer>().Compose(Yupi.GetGame().GetClientManager(), "Notice", $"You have all been summoned by\r- {session.GetHabbo().UserName}:\r\n{reason}", "", "", "", 4); 

            foreach (GameClient client in Yupi.GetGame().GetClientManager().Clients.Values)
            {
                if (session.GetHabbo().CurrentRoom == null ||
                    session.GetHabbo().CurrentRoomId == client.GetHabbo().CurrentRoomId)
                    continue;

                client.PrepareRoomForUser(session.GetHabbo().CurrentRoom.RoomId,
                        session.GetHabbo().CurrentRoom.RoomData.PassWord);
            }
            return true;
        }
    }
}