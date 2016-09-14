namespace Yupi.Messages.Contracts
{
    using System.Collections.Generic;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class SendAchievementsRequirementsMessageComposer : AbstractComposer<IDictionary<string, Achievement>>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, IDictionary<string, Achievement> achievements)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}