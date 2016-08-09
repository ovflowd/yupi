using System.Linq;
using Yupi.Protocol.Buffers;
using System.Collections.Generic;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
	public abstract class CatalogueIndexMessageComposer : AbstractComposer<IList<CatalogPage>, string>
	{
		public override void Compose(Yupi.Protocol.ISender session, IList<CatalogPage> sortedPages, string type)
		{
		 // Do nothing by default.
		}
	}
}
