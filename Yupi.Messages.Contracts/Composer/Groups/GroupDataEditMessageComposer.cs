namespace Yupi.Messages.Contracts
{
    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class GroupDataEditMessageComposer : AbstractComposer<Group>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, Group group)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}