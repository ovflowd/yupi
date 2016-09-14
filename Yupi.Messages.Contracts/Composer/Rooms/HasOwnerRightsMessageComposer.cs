namespace Yupi.Messages.Contracts
{
    using System;

    public abstract class HasOwnerRightsMessageComposer : AbstractComposerEmpty
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session)
        {
            // do nothing
        }

        #endregion Methods
    }
}