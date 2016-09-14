using System;


namespace Yupi.Messages.Rooms
{
	public class RefusePollMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			uint pollId = request.GetUInt32();

			/*
			session.GetHabbo().AnsweredPolls.Add(pollId);

			using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
			{
				queryReactor.SetQuery("INSERT INTO users_polls VALUES (@userid , @pollid , 0 , '0' , '')");
				queryReactor.AddParameter("userid", session.GetHabbo().Id);
				queryReactor.AddParameter("pollid", pollId);
				queryReactor.RunQuery();
			}
			*/
			throw new NotImplementedException ();
		}
	}
}

