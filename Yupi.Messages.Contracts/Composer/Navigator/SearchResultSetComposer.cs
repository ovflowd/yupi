using Yupi.Protocol.Buffers;
using System.Collections.Generic;
using System.Linq;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
	public abstract class SearchResultSetComposer : AbstractComposer<string, string>
	{
		public override void Compose(Yupi.Protocol.ISender session, string staticId, string query)
		{
		 // Do nothing by default.
		}
	}
}
