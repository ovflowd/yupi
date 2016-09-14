namespace Yupi.Messages.Contracts
{
    using System.Collections.Generic;

    using Yupi.Model.Domain;
    using Yupi.Net;
    using Yupi.Protocol.Buffers;

    public abstract class UserTagsMessageComposer : AbstractComposer<UserInfo>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, UserInfo info)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}