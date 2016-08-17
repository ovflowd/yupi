using System;

namespace Yupi.Messages.User
{
	public class UserGetVolumeSettingsMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			router.GetComposer<LoadVolumeMessageComposer> ().Compose (session, session.Info.Preferences);
		}
	}
}

