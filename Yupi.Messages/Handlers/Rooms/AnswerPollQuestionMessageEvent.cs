using System;
using System.Collections.Generic;
using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Rooms
{
    public class AnswerPollQuestionMessageEvent : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            var pollId = request.GetUInt32();
            var questionId = request.GetUInt32();
            var lineCount = request.GetInteger();

            var list = new List<string>();

            for (var i = 0; i < lineCount; i++)
                list.Add(request.GetString());

            // TODO Is a windows line carriage required?
            var text = string.Join("\r\n", list);

            /*
            Poll poll = Yupi.GetGame().GetPollManager().TryGetPollById(pollId);

            if (poll != null && poll.Type == PollType.Matching)
            {
                if (text == "1")
                    poll.AnswersPositive++;
                else
                    poll.AnswersNegative++;


                router.GetComposer<MatchingPollAnsweredMessageComposer> ().Compose (session, session.GetHabbo ().Id, text);
                session.GetHabbo().AnsweredPool = true;

                return;
            }

            session.GetHabbo().AnsweredPolls.Add(pollId);

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.SetQuery(
                    "INSERT INTO users_polls VALUES (@userid , @pollid , @questionid , '1' , @answer)");

                queryReactor.AddParameter("userid", session.GetHabbo().Id);
                queryReactor.AddParameter("pollid", pollId);
                queryReactor.AddParameter("questionid", questionId);
                queryReactor.AddParameter("answer", text);
                queryReactor.RunQuery();
            }
            */
            throw new NotImplementedException();
        }
    }
}