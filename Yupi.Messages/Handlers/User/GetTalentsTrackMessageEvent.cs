using System;
using System.Collections.Generic;
using Yupi.Emulator.Game.Achievements.Structs;

namespace Yupi.Messages.User
{
	public class GetTalentsTrackMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage message, Router router)
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

