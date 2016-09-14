namespace Yupi.Messages.Music
{
    using System;
    using System.Collections.Specialized;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public class SongsLibraryMessageComposer : Yupi.Messages.Contracts.SongsLibraryMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, SongItem[] songs)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(songs.Length);
                foreach (SongItem userItem in songs)
                {
                    message.AppendInteger(userItem.Id);
                    message.AppendInteger(userItem.Song.Id);
                }
                session.Send(message);
            }
        }

        #endregion Methods
    }
}