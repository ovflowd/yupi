namespace Yupi.Messages.Contracts
{
    using System;

    public abstract class DoorbellNoOneMessageComposer : AbstractComposerVoid
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}