namespace Yupi.Messages.Contracts
{
    using System.Collections.Generic;
    using System.Collections.Specialized;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class FlatCategoriesMessageComposer : AbstractComposer<IList<FlatNavigatorCategory>, int>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, IList<FlatNavigatorCategory> categories,
            int userRank)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}