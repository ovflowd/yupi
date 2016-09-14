using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Youtube
{
    public class YouTubeLoadVideoMessageComposer : Contracts.YouTubeLoadVideoMessageComposer
    {
        public override void Compose(ISender session, YoutubeTVItem tv)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(tv.Id);
                message.AppendString(tv.PlayingVideo.Id);
                message.AppendInteger(0); // TODO Probably strings (desc?)
                message.AppendInteger(0);
                message.AppendInteger(0);
                session.Send(message);
            }
        }
    }
}