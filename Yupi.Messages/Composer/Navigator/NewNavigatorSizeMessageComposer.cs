using System;

using Yupi.Protocol.Buffers;
using Yupi.Model.Domain.Components;

namespace Yupi.Messages.Navigator
{
	public class NewNavigatorSizeMessageComposer : AbstractComposer<UserPreferences>
	{
		public override void Compose (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, UserPreferences preferences)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(preferences.NewnaviX);
				message.AppendInteger(preferences.NewnaviY);
				message.AppendInteger(preferences.NavigatorWidth);
				message.AppendInteger(preferences.NavigatorHeight);
				message.AppendBool(false);
				message.AppendInteger(1);
				session.Send (message);
			}
		}
	}
}

