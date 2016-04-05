using System;
using Yupi.Emulator.Game.GameClients.Interfaces;

namespace Yupi.Messages.Guides
{
	public class OnGuideSessionDetachedMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage message, Router router)
		{
			bool state = message.GetBool();

			if (!state)
				return;

			GameClient requester = session.GetHabbo().GuideOtherUser;

			// TODO SessionStarted on Detach???
			router.GetComposer<OnGuideSessionStartedMessageComposer> ().Compose (session, requester.GetHabbo (), requester);
		}
	}
}

