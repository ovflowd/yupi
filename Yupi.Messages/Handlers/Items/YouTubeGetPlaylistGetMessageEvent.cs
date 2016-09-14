namespace Yupi.Messages.Items
{
    using System;

    using Yupi.Messages.Youtube;

    public class YouTubeGetPlaylistGetMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            uint itemId = request.GetUInt32();
            string video = request.GetString();

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

        #endregion Methods
    }
}