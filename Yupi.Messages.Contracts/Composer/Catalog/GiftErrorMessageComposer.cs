namespace Yupi.Messages.Contracts
{
    using System;

    public class GiftErrorMessageComposer : AbstractComposer<string>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, string username)
        {
        }

        #endregion Methods
    }
}