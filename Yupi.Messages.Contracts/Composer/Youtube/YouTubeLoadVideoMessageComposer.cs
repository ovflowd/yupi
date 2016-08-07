using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
	public abstract class YouTubeLoadVideoMessageComposer : AbstractComposer<YoutubeTVItem>
	{
		public override void Compose(Yupi.Protocol.ISender session, YoutubeTVItem tv)
		{
		 // Do nothing by default.
		}
	}
}
