namespace Yupi.Messages.Contracts
{
    using System.Collections.Generic;
    using System.Linq;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class SearchResultSetComposer : AbstractComposer<string, string, IDictionary<NavigatorCategory, IList<RoomData>>>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, string staticId, string query,
            IDictionary<NavigatorCategory, IList<RoomData>> result)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}