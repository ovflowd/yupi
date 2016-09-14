namespace Yupi.Messages.Contracts
{
    using System.Collections.Generic;
    using System.Linq;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class GroupForumListingsMessageComposer : AbstractComposer<int, int>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, int selectType, int startIndex)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}