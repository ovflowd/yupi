using System.Collections.Generic;
using Yupi.Protocol;

namespace Yupi.Messages.Rooms
{
    public class RoomLoadFilterMessageComposer : Contracts.RoomLoadFilterMessageComposer
    {
        public override void Compose(ISender session, List<string> wordlist)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(wordlist.Count);

                foreach (var word in wordlist) message.AppendString(word);

                session.Send(message);
            }
        }
    }
}