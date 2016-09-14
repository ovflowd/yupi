using System;
using Yupi.Protocol;

namespace Yupi.Messages.Catalog
{
    public class GiftWrappingConfigurationMessageComposer : Contracts.GiftWrappingConfigurationMessageComposer
    {
        public override void Compose(ISender session)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                throw new NotImplementedException();
            }
        }
    }
}