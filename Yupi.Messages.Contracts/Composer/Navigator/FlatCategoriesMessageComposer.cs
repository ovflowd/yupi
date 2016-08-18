using System.Collections.Specialized;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;
using System.Collections.Generic;

namespace Yupi.Messages.Contracts
{
	public abstract class FlatCategoriesMessageComposer : AbstractComposer<IList<FlatNavigatorCategory>, int>
	{
		public override void Compose(Yupi.Protocol.ISender session, IList<FlatNavigatorCategory> categories, int userRank)
		{
		 // Do nothing by default.
		}
	}
}
