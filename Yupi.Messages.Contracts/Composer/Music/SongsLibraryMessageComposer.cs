using System.Collections.Specialized;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
	public abstract class SongsLibraryMessageComposer : AbstractComposer<HybridDictionary>
	{
		public override void Compose(Yupi.Protocol.ISender session, HybridDictionary songs)
		{
		 // Do nothing by default.
		}
	}
}
