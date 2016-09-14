namespace Yupi.Messages.Navigator
{
    using System;
    using System.Collections.Generic;

    using Yupi.Controller;
    using Yupi.Model.Domain;

    public class SearchNewNavigatorEvent : AbstractHandler
    {
        #region Methods

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

        #endregion Methods
    }
}