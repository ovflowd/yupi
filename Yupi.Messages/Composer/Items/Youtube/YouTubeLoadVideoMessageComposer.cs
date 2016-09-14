namespace Yupi.Messages.Youtube
{
    using System;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public class YouTubeLoadVideoMessageComposer : Yupi.Messages.Contracts.YouTubeLoadVideoMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, YoutubeTVItem tv)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(tv.Id);
                message.AppendString(tv.PlayingVideo.Id);
                message.AppendInteger(0); // TODO Probably strings (desc?)
                message.AppendInteger(0);
                message.AppendInteger(0);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}