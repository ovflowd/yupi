namespace Yupi.Messages.Contracts
{
    using System.Collections.Generic;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class TargetedOfferMessageComposer : AbstractComposer<TargetedOffer>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, TargetedOffer offer)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}