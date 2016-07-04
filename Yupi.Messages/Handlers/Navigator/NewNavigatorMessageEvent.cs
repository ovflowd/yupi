using System;

namespace Yupi.Messages.Navigator
{
	public class NewNavigatorMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			if (session == null)
				return;

			router.GetComposer<NavigatorMetaDataComposer> ().Compose (session);
			router.GetComposer<NavigatorLiftedRoomsComposer> ().Compose (session, Yupi.GetGame().GetNavigator().NavigatorHeaders);
			router.GetComposer<NavigatorCategorys> ().Compose (session, Yupi.GetGame ().GetNavigator ());
			router.GetComposer<NavigatorSavedSearchesComposer> ().Compose (session, session.GetHabbo().NavigatorLogs);
			router.GetComposer<NewNavigatorSizeMessageComposer> ().Compose (session, session.GetHabbo ().Preferences);
		}
	}
}

