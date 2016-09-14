using System.Collections.Generic;
using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class SearchResultSetComposer :
        AbstractComposer<string, string, IDictionary<NavigatorCategory, IList<RoomData>>>
    {
        public override void Compose(ISender session, string staticId, string query,
            IDictionary<NavigatorCategory, IList<RoomData>> result)
        {
            // Do nothing by default.
        }
    }
}