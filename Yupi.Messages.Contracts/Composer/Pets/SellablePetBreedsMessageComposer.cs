using Yupi.Protocol.Buffers;
using System.Collections.Generic;

namespace Yupi.Messages.Contracts
{
    public abstract class SellablePetBreedsMessageComposer : AbstractComposer<string>
    {
        public override void Compose(Yupi.Protocol.ISender session, string type)
        {
            // Do nothing by default.
        }
    }
}