namespace Yupi.Messages.Contracts
{
    using System.Collections.Generic;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class NavigatorSavedSearchesComposer : AbstractComposer<IList<UserSearchLog>>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, IList<UserSearchLog> searchLog)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}