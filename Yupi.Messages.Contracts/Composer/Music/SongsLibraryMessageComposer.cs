using System.Collections.Specialized;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
	public abstract class SongsLibraryMessageComposer : AbstractComposer<SongItem[]>
	{
		public override void Compose(Yupi.Protocol.ISender session, SongItem[] songs)
		{
		 // Do nothing by default.
		}
	}
}
