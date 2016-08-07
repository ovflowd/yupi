using Yupi.Protocol.Buffers;
using System.Collections.Generic;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
	public abstract class JukeboxPlaylistMessageComposer : AbstractComposer<int, IList<SongItem>>
	{
		public override void Compose(Yupi.Protocol.ISender session, int capacity, IList<SongItem> playlist)
		{
		 // Do nothing by default.
		}
	}
}
