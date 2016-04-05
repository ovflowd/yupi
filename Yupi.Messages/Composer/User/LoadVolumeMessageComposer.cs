using System;
using Yupi.Protocol.Buffers;
using Yupi.Emulator.Game.GameClients.Interfaces;

namespace Yupi.Messages.User
{
	public class LoadVolumeMessageComposer : AbstractComposer<UserPreferences>
	{
		public override void Compose (GameClient session, UserPreferences preferences)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendIntegersArray(preferences.Volume, ',', 3, 0, 100);
				message.AppendBool(preferences.PreferOldChat);
				message.AppendBool(preferences.IgnoreRoomInvite);
				message.AppendBool(preferences.DisableCameraFollow);
				// TODO Add to preferences
				message.AppendInteger(3); // collapse friends (3 = no) 
				message.AppendInteger(preferences.ChatColor); //bubble

				session.Send(message);
			}
		}
	}
}

