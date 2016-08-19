using Yupi.Protocol.Buffers;
using System.Collections.Generic;
using System.Linq;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
	public abstract class SearchResultSetComposer : AbstractComposer<string, string, IList<SearchResultEntry>>
	{
		public override void Compose(Yupi.Protocol.ISender session, string staticId, string query, IList<SearchResultEntry> result)
		{
		 // Do nothing by default.
		}
	}
}
