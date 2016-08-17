using System;

namespace Yupi.Messages.Navigator
{
	public class SearchNewNavigatorEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			if (session == null)
				return;

			string name = request.GetString();
			// TODO What???
			string junk = request.GetString();

			router.GetComposer<SearchResultSetComposer> ().Compose (session, name, junk);
		}
	}
}

