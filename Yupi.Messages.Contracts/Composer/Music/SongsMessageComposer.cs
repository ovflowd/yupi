using System.Collections.Generic;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
	public abstract class SongsMessageComposer : AbstractComposer<List<SongData>>
	{
		public override void Compose(Yupi.Protocol.ISender session, List<SongData> songs)
		{
		 // Do nothing by default.
		}
	}
}
