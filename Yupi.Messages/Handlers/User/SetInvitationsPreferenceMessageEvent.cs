using System;


namespace Yupi.Messages.User
{
	public class SetInvitationsPreferenceMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Net.ISession<GameClient> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			bool ignoreInvites = Request.GetBool();
			session.GetHabbo().Preferences.IgnoreRoomInvite = ignoreInvites;
			session.GetHabbo().Preferences.Save();
		}
	}
}

