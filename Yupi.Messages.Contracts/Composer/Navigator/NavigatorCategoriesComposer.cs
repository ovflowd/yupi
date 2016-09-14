namespace Yupi.Messages.Contracts
{
    using System.Collections.Generic;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class NavigatorCategoriesComposer : AbstractComposer<IList<NavigatorCategory>>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, IList<NavigatorCategory> categories)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}