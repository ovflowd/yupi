using Yupi.Controller;
using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Navigator
{
    public class SearchNewNavigatorEvent : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            var staticId = request.GetString();
            var query = request.GetString();

            var view = NavigatorView.FromValue(staticId);

            // TODO Create SearchResult class instead of Dictionary
            var categories = view.GetCategories(query, session.Info);
            router.GetComposer<SearchResultSetComposer>().Compose(session, staticId, query, categories);
        }
    }
}