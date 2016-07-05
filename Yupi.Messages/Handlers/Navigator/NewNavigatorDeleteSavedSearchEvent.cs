using System;

namespace Yupi.Messages.Navigator
{
	public class NewNavigatorDeleteSavedSearchEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			int searchId = request.GetInteger();

			if (!session.GetHabbo().NavigatorLogs.ContainsKey(searchId))
				return;

			session.GetHabbo().NavigatorLogs.Remove(searchId);

			NavigatorSavedSearchesComposer.Compose(session);
		}
	}
}

