namespace Yupi.Messages.Music
{
    using System;

    using Yupi.Protocol.Buffers;

    // TODO Refactor?
    public class JukeboxNowPlayingMessageComposer : Yupi.Messages.Contracts.JukeboxNowPlayingMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, uint songId, int playlistPosition, int songPosition)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(songId);
                message.AppendInteger(playlistPosition);
                message.AppendInteger(songId);
                message.AppendInteger(0);
                message.AppendInteger(songPosition); // songPosition in ms
                session.Send(message);
            }
        }

        #endregion Methods
    }
}