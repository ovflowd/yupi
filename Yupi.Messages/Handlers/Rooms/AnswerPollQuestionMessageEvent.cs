using System;
using System.Collections.Generic;




namespace Yupi.Messages.Rooms
{
	public class AnswerPollQuestionMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
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
			throw new NotImplementedException ();
		}
	}
}

