namespace Yupi.Messages.Contracts
{
    using System;
    using System.Collections.Generic;

    using Yupi.Model.Domain;

    public class PetInventoryMessageComposer : AbstractComposer<IList<PetItem>>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, IList<PetItem> pets)
        {
        }

        #endregion Methods
    }
}