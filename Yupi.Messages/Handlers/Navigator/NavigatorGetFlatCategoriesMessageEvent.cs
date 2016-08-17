using System;

namespace Yupi.Messages.Navigator
{
	public class NavigatorGetFlatCategoriesMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			throw new NotImplementedException ();
			//router.GetComposer<FlatCategoriesMessageComposer> ().Compose (session, Yupi.GetGame ().GetNavigator ().PrivateCategories, session.Info.Rank);
		}
	}
}

