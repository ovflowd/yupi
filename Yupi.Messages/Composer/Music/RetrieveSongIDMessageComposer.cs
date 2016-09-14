namespace Yupi.Messages.Music
{
    using System;

    using Yupi.Protocol.Buffers;

    public class RetrieveSongIDMessageComposer : Yupi.Messages.Contracts.RetrieveSongIDMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, string name, int songId)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendString(name);
                message.AppendInteger(songId);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}