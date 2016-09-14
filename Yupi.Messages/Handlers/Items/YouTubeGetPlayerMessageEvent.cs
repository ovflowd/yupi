namespace Yupi.Messages.Items
{
    using System;
    using System.Collections.Generic;

    using Yupi.Messages.Youtube;

    public class YouTubeGetPlayerMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            uint itemId = request.GetUInt32();

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

        #endregion Methods
    }
}