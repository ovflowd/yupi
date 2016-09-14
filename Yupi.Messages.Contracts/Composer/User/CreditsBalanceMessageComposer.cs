namespace Yupi.Messages.Contracts
{
    using System;

    using Yupi.Model.Domain.Components;

    public class CreditsBalanceMessageComposer : AbstractComposer<int>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, int credits)
        {
        }

        #endregion Methods
    }
}