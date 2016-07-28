using System;


namespace Yupi.Messages.User
{
	public class SetInvitationsPreferenceMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			bool ignoreInvites = Request.GetBool();
			session.GetHabbo().Preferences.IgnoreRoomInvite = ignoreInvites;
			session.GetHabbo().Preferences.Save();
		}
	}
}

