using System;
using Yupi.Messages.Contracts;
using Yupi.Util;



namespace Yupi.Messages.User
{
	public class OnlineConfirmationMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			string username = request.GetString();

			// TODO Implement
			//session.Router.GetComposer<HotelClosedMessageComposer> ().Compose (session, 8, 0, false);

			if (username != session.Info.UserName) {
				// TODO Should we check this and disconnect on mismatch?
			}

			// TODO Welcome message
			//router.GetComposer<SuperNotificationMessageComposer>().Compose(session, T._("Welcome"), T._("This is a development build"));
			/*
			YupiWriterManager.WriteLine(request.GetString() + " joined game. With IP " + session.RemoteAddress, "Yupi.User", ConsoleColor.DarkGreen);

			if (!ServerConfigurationSettings.Data.ContainsKey("welcome.message.enabled") ||
				ServerConfigurationSettings.Data["welcome.message.enabled"] != "true")
				return;

			if (!ServerConfigurationSettings.Data.ContainsKey("welcome.message.image") ||
				string.IsNullOrEmpty(ServerConfigurationSettings.Data["welcome.message.image"]))
				session.SendNotifWithScroll(ServerExtraSettings.WelcomeMessage.Replace("%username%",
					session.GetHabbo().UserName));
			else
				session.SendNotif(ServerExtraSettings.WelcomeMessage.Replace("%username%", session.GetHabbo().UserName),
					ServerConfigurationSettings.Data.ContainsKey("welcome.message.title")
					? ServerConfigurationSettings.Data["welcome.message.title"]
					: string.Empty, ServerConfigurationSettings.Data["welcome.message.image"]);
					*/
		}
	}
}

