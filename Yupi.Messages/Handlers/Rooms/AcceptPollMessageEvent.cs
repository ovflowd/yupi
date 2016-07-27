using System;


namespace Yupi.Messages.Rooms
{
	public class AcceptPollMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Net.ISession<IGameClient> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			uint pollId = request.GetUInt32();
			// TODO Unchecked array access!
			Poll poll = Yupi.GetGame().GetPollManager().Polls[pollId];

			router.GetComposer<PollQuestionsMessageComposer> ().Compose (session, poll);
		}
	}
}

