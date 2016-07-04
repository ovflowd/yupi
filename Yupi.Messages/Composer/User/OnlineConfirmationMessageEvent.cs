using System;
using Yupi.Emulator.Core.Io.Logger;
using Yupi.Emulator.Core.Settings;

namespace Yupi.Messages.User
{
	public class OnlineConfirmationMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			YupiWriterManager.WriteLine(request.GetString() + " joined game. With IP " + session.GetConnection().RemoteAddress, "Yupi.User", ConsoleColor.DarkGreen);

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
		}
	}
}

