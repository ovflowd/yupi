using System.Collections.Generic;
using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class NavigatorCategoriesComposer : AbstractComposer<IList<NavigatorCategory>>
    {
        public override void Compose(ISender session, IList<NavigatorCategory> categories)
        {
            // Do nothing by default.
        }
    }
}