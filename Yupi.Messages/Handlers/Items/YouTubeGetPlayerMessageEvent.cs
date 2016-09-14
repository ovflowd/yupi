using System;
using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Items
{
    public class YouTubeGetPlayerMessageEvent : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            var itemId = request.GetUInt32();

            /*
            RoomItem item = session.GetHabbo().CurrentRoom.GetRoomItemHandler().GetItem(itemId);

            if (item == null)
                return;

            Dictionary<string, YoutubeVideo> videos = session.GetHabbo().GetYoutubeManager().Videos;

            if (videos == null)
                return;

            router.GetComposer<YouTubeLoadVideoMessageComposer> ().Compose (session, item);
            router.GetComposer<YouTubeLoadPlaylistsMessageComposer> ().Compose (session, item, videos);
            */
            throw new NotImplementedException();
        }
    }
}