using System;


namespace Yupi.Messages.User
{
	public class SetChatPreferenceMessageEvent : AbstractHandler
	{
		public override void HandleMessage (GameClient session, Yupi.Protocol.Buffers.ClientMessage message, Router router)
		{
			bool preferOldChat = message.GetBool();
			session.GetHabbo().Preferences.PreferOldChat = preferOldChat;
			session.GetHabbo().Preferences.Save();
		}
	}
}

