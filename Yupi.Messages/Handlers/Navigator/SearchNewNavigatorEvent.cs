using System;
using Yupi.Controller;
using System.Collections.Generic;
using Yupi.Model.Domain;

namespace Yupi.Messages.Navigator
{
    public class SearchNewNavigatorEvent : AbstractHandler
    {
        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            string staticId = request.GetString();
            string query = request.GetString();

            NavigatorView view = NavigatorView.FromValue(staticId);

            // TODO Create SearchResult class instead of Dictionary
            IDictionary<NavigatorCategory, IList<RoomData>> categories = view.GetCategories(query, session.Info);
            router.GetComposer<SearchResultSetComposer>().Compose(session, staticId, query, categories);
        }
    }
}