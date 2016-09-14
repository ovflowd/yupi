namespace Yupi.Messages.Contracts
{
    using System.Linq;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class RelationshipMessageComposer : AbstractComposer<UserInfo>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, UserInfo habbo)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}