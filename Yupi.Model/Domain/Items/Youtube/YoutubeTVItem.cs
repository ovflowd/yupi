using System;

namespace Yupi.Model.Domain
{
	public class YoutubeTVItem : FloorItem<YoutubeTvBaseItem>
	{
		public virtual YoutubeVideo PlayingVideo { get; set; }
	}
}

