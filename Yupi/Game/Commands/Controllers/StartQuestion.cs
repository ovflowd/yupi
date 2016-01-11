using System.Collections.Generic;
using System.Threading;
using Yupi.Game.Commands.Interfaces;
using Yupi.Game.GameClients.Interfaces;
using Yupi.Game.Polls;
using Yupi.Game.Polls.Enums;
using Yupi.Game.Rooms;
using Yupi.Game.Rooms.User;
using Yupi.Game.Users;
using Yupi.Messages;
using Yupi.Messages.Parsers;

namespace Yupi.Game.Commands.Controllers
{
    /// <summary>
    ///     Class StartQuestion. This class cannot be inherited.
    /// </summary>
    internal sealed class StartQuestion : Command
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

        internal static void MatchingPollAnswer(GameClient client, Poll poll)
        {
            if (poll == null || poll.Type != PollType.Matching)
                return;
            ServerMessage message = new ServerMessage(LibraryParser.OutgoingRequest("MatchingPollMessageComposer"));
            message.AppendString("MATCHING_POLL");
            message.AppendInteger(poll.Id);
            message.AppendInteger(poll.Id);
            message.AppendInteger(15580);
            message.AppendInteger(poll.Id);
            message.AppendInteger(29);
            message.AppendInteger(5);
            message.AppendString(poll.PollName);
            client.GetHabbo().CurrentRoom.SendMessage(message);
        }

        internal static void MatchingPollResults(GameClient client, Poll poll)
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
                        ServerMessage result =
                            new ServerMessage(LibraryParser.OutgoingRequest("MatchingPollResultMessageComposer"));
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