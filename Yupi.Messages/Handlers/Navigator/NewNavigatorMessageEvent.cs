using System;

namespace Yupi.Messages.Navigator
{
	public class NewNavigatorMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			if (session == null)
				return;

			router.GetComposer<NavigatorMetaDataComposer> ().Compose (session);
			router.GetComposer<NavigatorLiftedRoomsComposer> ().Compose (session);
			router.GetComposer<NavigatorCategorys> ().Compose (session, Yupi.GetGame ().GetNavigator ());
			router.GetComposer<NavigatorSavedSearchesComposer> ().Compose (session, session.GetHabbo().NavigatorLogs);
			router.GetComposer<NewNavigatorSizeMessageComposer> ().Compose (session, session.GetHabbo ().Preferences);
		}
	}
}

