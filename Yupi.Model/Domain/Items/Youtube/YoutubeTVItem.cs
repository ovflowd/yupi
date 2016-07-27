using System;

namespace Yupi.Model.Domain
{
	public class YoutubeTVItem : Item<YoutubeTvBaseItem>
	{
		public virtual YoutubeVideo PlayingVideo { get; set; }
	}
}

