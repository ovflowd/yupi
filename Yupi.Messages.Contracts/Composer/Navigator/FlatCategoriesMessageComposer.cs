using System.Collections.Specialized;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;
using System.Collections.Generic;

namespace Yupi.Messages.Contracts
{
	public abstract class FlatCategoriesMessageComposer : AbstractComposer<IList<NavigatorCategory>, int>
	{
		public override void Compose(Yupi.Protocol.ISender session, IList<NavigatorCategory> categories, int userRank)
		{
		 // Do nothing by default.
		}
	}
}
