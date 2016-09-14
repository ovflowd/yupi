using System;
using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Items
{
    public class YouTubeGetPlaylistGetMessageEvent : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            var itemId = request.GetUInt32();
            var video = request.GetString();

            /*
            RoomItem item = session.GetHabbo().CurrentRoom.GetRoomItemHandler().GetItem(itemId);

            if (item.GetBaseItem().InteractionType != Interaction.YoutubeTv)
                return;

            if (!session.GetHabbo().GetYoutubeManager().Videos.ContainsKey(video))
                return;

            item.ExtraData = video;
            item.UpdateState();

            router.GetComposer<YouTubeLoadVideoMessageComposer> ().Compose (session, item);
            */
            throw new NotImplementedException();
        }
    }
}