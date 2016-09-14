using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;
using System.Collections.Generic;

namespace Yupi.Messages.Contracts
{
    public abstract class NavigatorCategoriesComposer : AbstractComposer<IList<NavigatorCategory>>
    {
        public override void Compose(Yupi.Protocol.ISender session, IList<NavigatorCategory> categories)
        {
            // Do nothing by default.
        }
    }
}