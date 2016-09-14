using System.Collections.Generic;
using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Music
{
    public class SongsMessageComposer : Contracts.SongsMessageComposer
    {
        public override void Compose(ISender session, List<SongData> songs)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(songs.Count);


                foreach (var current in songs)
                {
                    message.AppendInteger(current.Id);
                    message.AppendString(current.CodeName);
                    message.AppendString(current.Name);
                    message.AppendString(current.Data);
                    message.AppendInteger(current.LengthMiliseconds);
                    message.AppendString(current.Artist);
                }

                session.Send(message);
            }
        }
    }
}