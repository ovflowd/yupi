using System.Collections.Generic;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class RoomLoadFilterMessageComposer : AbstractComposer<List<string>>
    {
        public override void Compose(ISender session, List<string> wordlist)
        {
            // Do nothing by default.
        }
    }
}