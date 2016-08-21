using Yupi.Protocol.Buffers;
using System.Collections.Generic;
using System.Linq;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
	public abstract class SearchResultSetComposer : AbstractComposer<string, string, IDictionary<NavigatorCategory, IList<RoomData>>>
	{
		public override void Compose(Yupi.Protocol.ISender session, string staticId, string query, IDictionary<NavigatorCategory, IList<RoomData>> result)
		{
		 // Do nothing by default.
		}
	}
}
