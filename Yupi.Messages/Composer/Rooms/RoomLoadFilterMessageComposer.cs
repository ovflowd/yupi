namespace Yupi.Messages.Rooms
{
    using System;
    using System.Collections.Generic;

    using Yupi.Protocol.Buffers;

    public class RoomLoadFilterMessageComposer : Yupi.Messages.Contracts.RoomLoadFilterMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, List<string> wordlist)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(wordlist.Count);

                foreach (string word in wordlist)
                {
                    message.AppendString(word);
                }

                session.Send(message);
            }
        }

        #endregion Methods
    }
}