using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class YouTubeLoadVideoMessageComposer : AbstractComposer<YoutubeTVItem>
    {
        public override void Compose(ISender session, YoutubeTVItem tv)
        {
            // Do nothing by default.
        }
    }
}