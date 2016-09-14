namespace Yupi.Messages.Contracts
{
    using System.Collections.Generic;

    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class LoadWardrobeMessageComposer : AbstractComposer<IList<WardrobeItem>>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, IList<WardrobeItem> wardrobe)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}