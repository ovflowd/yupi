// ---------------------------------------------------------------------------------
// <copyright file="StartQuestion.cs" company="https://github.com/sant0ro/Yupi">
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
using System.Collections.Generic;
using System.Threading;
using Yupi.Emulator.Game.Commands.Interfaces;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Polls;
using Yupi.Emulator.Game.Polls.Enums;
using Yupi.Emulator.Game.Rooms;
using Yupi.Emulator.Game.Rooms.User;
using Yupi.Emulator.Game.Users;



namespace Yupi.Emulator.Game.Commands.Controllers
{
    /// <summary>
    ///     Class StartQuestion. This class cannot be inherited.
    /// </summary>
     public sealed class StartQuestion : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="StartQuestion" /> class.
        /// </summary>
        public StartQuestion()
        {
            MinRank = 7;
            Description = "Starts a matching question.";
            Usage = ":startquestion [id]";
            MinParams = 1;
        }

        public override bool Execute(GameClient client, string[] pms)
        {
            uint id = uint.Parse(pms[0]);
            Poll poll = Yupi.GetGame().GetPollManager().TryGetPollById(id);
            if (poll == null || poll.Type != PollType.Matching)
            {
                client.SendWhisper("Poll doesn't exists or isn't a matching poll.");
                return true;
            }
            poll.AnswersPositive = 0;
            poll.AnswersNegative = 0;
            MatchingPollAnswer(client, poll);
            Thread showPoll = new Thread(delegate() { MatchingPollResults(client, poll); });
            showPoll.Start();
            return true;
        }

     public static void MatchingPollAnswer(GameClient client, Poll poll)
        {
            if (poll == null || poll.Type != PollType.Matching)
                return;
            SimpleServerMessageBuffer messageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("MatchingPollMessageComposer"));
            messageBuffer.AppendString("MATCHING_POLL");
            messageBuffer.AppendInteger(poll.Id);
            messageBuffer.AppendInteger(poll.Id);
            messageBuffer.AppendInteger(15580);
            messageBuffer.AppendInteger(poll.Id);
            messageBuffer.AppendInteger(29);
            messageBuffer.AppendInteger(5);
            messageBuffer.AppendString(poll.PollName);
            client.GetHabbo().CurrentRoom.SendMessage(messageBuffer);
        }

     public static void MatchingPollResults(GameClient client, Poll poll)
        {
            Room room = client.GetHabbo().CurrentRoom;
            if (poll == null || poll.Type != PollType.Matching || room == null)
                return;

            HashSet<RoomUser> users = room.GetRoomUserManager().GetRoomUsers();

            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(1000);
                foreach (RoomUser roomUser in users)
                {
                    Habbo user = Yupi.GetHabboById(roomUser.UserId);
                    if (user.AnsweredPool)
                    {
                        SimpleServerMessageBuffer result =
                            new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("MatchingPollResultMessageComposer"));
                        result.AppendInteger(poll.Id);
                        result.AppendInteger(2);
                        result.AppendString("0");
                        result.AppendInteger(poll.AnswersNegative);
                        result.AppendString("1");
                        result.AppendInteger(poll.AnswersPositive);
                        client.SendMessage(result);
                    }
                }
            }

            foreach (RoomUser roomUser in users)
                Yupi.GetHabboById(roomUser.UserId).AnsweredPool = false;
        }
    }
}