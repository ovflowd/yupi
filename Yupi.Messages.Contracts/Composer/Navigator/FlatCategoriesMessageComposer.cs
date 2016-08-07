using System.Collections.Specialized;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
	public abstract class FlatCategoriesMessageComposer : AbstractComposer<HybridDictionary, int>
	{
		public override void Compose(Yupi.Protocol.ISender session, HybridDictionary categories, int userRank)
		{
		 // Do nothing by default.
		}
	}
}
