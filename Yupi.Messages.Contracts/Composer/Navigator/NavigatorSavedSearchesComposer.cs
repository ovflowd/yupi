using System.Collections.Generic;
using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class NavigatorSavedSearchesComposer : AbstractComposer<IList<UserSearchLog>>
    {
        public override void Compose(ISender session, IList<UserSearchLog> searchLog)
        {
            // Do nothing by default.
        }
    }
}