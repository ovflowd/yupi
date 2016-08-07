using System;


namespace Yupi.Messages.User
{
	public class SaveClientSettingsMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			// TODO Find the exact meaning of these values
			int num = message.GetInteger();
			int num2 = message.GetInteger();
			int num3 = message.GetInteger();
			session.UserData.GetHabbo().Preferences.Volume = num.ToString() + "," + num2.ToString() + "," + num3.ToString();
			session.UserData.GetHabbo().Preferences.Save();
		}
	}
}

