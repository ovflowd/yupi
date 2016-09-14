// ---------------------------------------------------------------------------------
// <copyright file="AnswerPollQuestionMessageEvent.cs" company="https://github.com/sant0ro/Yupi">
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
    using System.Collections.Generic;

    public class AnswerPollQuestionMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            uint pollId = request.GetUInt32();
            uint questionId = request.GetUInt32();
            int lineCount = request.GetInteger();

            List<string> list = new List<string>();

            for (int i = 0; i < lineCount; i++)
                list.Add(request.GetString());

            // TODO Is a windows line carriage required?
            string text = string.Join("\r\n", list);

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

        #endregion Methods
    }
}