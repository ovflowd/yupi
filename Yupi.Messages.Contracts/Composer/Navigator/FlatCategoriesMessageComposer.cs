using System.Collections.Generic;
using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class FlatCategoriesMessageComposer : AbstractComposer<IList<FlatNavigatorCategory>, int>
    {
        public override void Compose(ISender session, IList<FlatNavigatorCategory> categories, int userRank)
        {
            // Do nothing by default.
        }
    }
}