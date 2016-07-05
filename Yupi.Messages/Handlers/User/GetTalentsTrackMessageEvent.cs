using System;
using System.Collections.Generic;


namespace Yupi.Messages.User
{
	public class GetTalentsTrackMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Net.ISession<GameClient> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			string trackType = message.GetString();
			// TODO Magic constant
			List<Talent> talents = Yupi.GetGame().GetTalentManager().GetTalents(trackType, -1);

			if (talents == null)
				return;

			router.GetComposer<TalentsTrackMessageComposer> ().Compose (session, trackType, talents);
		}
	}
}

