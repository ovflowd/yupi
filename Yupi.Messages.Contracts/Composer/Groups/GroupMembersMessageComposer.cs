namespace Yupi.Messages.Contracts
{
    using System.Collections.Generic;
    using System.Linq;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class GroupMembersMessageComposer : AbstractComposer<UserInfo, Group>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, UserInfo user, Group group)
        {
            // Do nothing by default.
        }

        public virtual void Compose(Yupi.Protocol.ISender session, Group group, uint reqType, UserInfo user,
            string searchVal = "", int page = 0)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}