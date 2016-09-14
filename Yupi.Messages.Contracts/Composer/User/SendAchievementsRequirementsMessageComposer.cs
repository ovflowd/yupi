using System.Collections.Generic;
using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class SendAchievementsRequirementsMessageComposer :
        AbstractComposer<IDictionary<string, Achievement>>
    {
        public override void Compose(ISender session, IDictionary<string, Achievement> achievements)
        {
            // Do nothing by default.
        }
    }
}