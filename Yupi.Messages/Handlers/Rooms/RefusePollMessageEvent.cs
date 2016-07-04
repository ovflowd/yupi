using System;
using Yupi.Emulator.Data.Base.Adapters.Interfaces;

namespace Yupi.Messages.Rooms
{
	public class RefusePollMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			uint pollId = request.GetUInt32();

			session.GetHabbo().AnsweredPolls.Add(pollId);

			using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
			{
				queryReactor.SetQuery("INSERT INTO users_polls VALUES (@userid , @pollid , 0 , '0' , '')");
				queryReactor.AddParameter("userid", session.GetHabbo().Id);
				queryReactor.AddParameter("pollid", pollId);
				queryReactor.RunQuery();
			}
		}
	}
}

