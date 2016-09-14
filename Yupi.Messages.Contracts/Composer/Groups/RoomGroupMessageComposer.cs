namespace Yupi.Messages.Contracts
{
    using System.Collections.Generic;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class RoomGroupMessageComposer : AbstractComposer<ISet<Group>>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender room, ISet<Group> groups)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}