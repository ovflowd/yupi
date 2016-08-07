using System.Linq;
using Yupi.Protocol.Buffers;
using System.Collections.Generic;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
	public abstract class CatalogueIndexMessageComposer : AbstractComposer<IOrderedEnumerable<CatalogPage>, IEnumerable<CatalogPage>, string>
	{
		public override void Compose(Yupi.Protocol.ISender session, IOrderedEnumerable<CatalogPage> sortedPages, IEnumerable<CatalogPage> pages, string type)
		{
		 // Do nothing by default.
		}
	}
}
