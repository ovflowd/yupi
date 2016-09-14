namespace Yupi.Messages.Contracts
{
    using System;

    public class RemoveInventoryObjectMessageComposer : AbstractComposer<int>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, int itemId)
        {
        }

        #endregion Methods
    }
}