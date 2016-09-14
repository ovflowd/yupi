namespace Yupi.Messages.Contracts
{
    using System.Collections.Generic;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class EffectsInventoryMessageComposer : AbstractComposer<IList<AvatarEffect>>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, IList<AvatarEffect> effects)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}