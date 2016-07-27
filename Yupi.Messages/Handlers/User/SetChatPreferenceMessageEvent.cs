using System;


namespace Yupi.Messages.User
{
	public class SetChatPreferenceMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Net.ISession<IGameClient> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			bool preferOldChat = message.GetBool();
			session.GetHabbo().Preferences.PreferOldChat = preferOldChat;
			session.GetHabbo().Preferences.Save();
		}
	}
}

