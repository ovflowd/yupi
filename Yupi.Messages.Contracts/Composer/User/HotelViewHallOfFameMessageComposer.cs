using Yupi.Protocol.Buffers;
using System.Collections.Generic;

namespace Yupi.Messages.Contracts
{
    public abstract class HotelViewHallOfFameMessageComposer : AbstractComposer<string>
    {
        public override void Compose(Yupi.Protocol.ISender session, string code)
        {
            // Do nothing by default.
        }
    }
}