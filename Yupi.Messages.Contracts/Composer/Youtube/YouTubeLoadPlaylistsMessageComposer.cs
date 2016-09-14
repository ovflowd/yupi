using Yupi.Protocol;
using System.Collections.Generic;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
	public abstract class YouTubeLoadPlaylistsMessageComposer : AbstractComposer<YoutubeTVItem, Dictionary<string, YoutubeVideo>>
	{
		public override void Compose(Yupi.Protocol.ISender session, YoutubeTVItem tv, Dictionary<string, YoutubeVideo> videos)
		{
		 // Do nothing by default.
		}
	}
}
