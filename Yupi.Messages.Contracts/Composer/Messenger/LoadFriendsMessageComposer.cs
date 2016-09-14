namespace Yupi.Messages.Contracts
{
    using System.Collections.Generic;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class LoadFriendsMessageComposer : AbstractComposer<IList<Relationship>>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, IList<Relationship> friends)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}