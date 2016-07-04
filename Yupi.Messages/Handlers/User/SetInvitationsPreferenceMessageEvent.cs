using System;
using Yupi.Emulator.Game.GameClients.Interfaces;

namespace Yupi.Messages.User
{
	public class SetInvitationsPreferenceMessageEvent : AbstractHandler
	{
		public override void HandleMessage (GameClient session, Yupi.Protocol.Buffers.ClientMessage message, Router router)
		{
			bool ignoreInvites = Request.GetBool();
			session.GetHabbo().Preferences.IgnoreRoomInvite = ignoreInvites;
			session.GetHabbo().Preferences.Save();
		}
	}
}

