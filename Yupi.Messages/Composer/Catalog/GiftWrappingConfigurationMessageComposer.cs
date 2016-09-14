namespace Yupi.Messages.Catalog
{
    using System;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public class GiftWrappingConfigurationMessageComposer : Yupi.Messages.Contracts.GiftWrappingConfigurationMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                throw new NotImplementedException();
            }
        }

        #endregion Methods
    }
}