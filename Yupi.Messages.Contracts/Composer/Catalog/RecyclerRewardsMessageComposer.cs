namespace Yupi.Messages.Contracts
{
    using System.Collections.Generic;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class RecyclerRewardsMessageComposer : AbstractComposer<EcotronLevel[]>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, EcotronLevel[] levels)
        {
        }

        #endregion Methods
    }
}