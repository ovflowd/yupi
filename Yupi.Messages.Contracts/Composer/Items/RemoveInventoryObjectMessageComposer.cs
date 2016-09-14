using System;

namespace Yupi.Messages.Contracts
{
    public class RemoveInventoryObjectMessageComposer : AbstractComposer<int>
    {
        public override void Compose(Yupi.Protocol.ISender session, int itemId)
        {
        }
    }
}