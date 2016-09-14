using System.Collections.Generic;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
    public abstract class SendAchievementsRequirementsMessageComposer :
        AbstractComposer<IDictionary<string, Achievement>>
    {
        public override void Compose(Yupi.Protocol.ISender session, IDictionary<string, Achievement> achievements)
        {
            // Do nothing by default.
        }
    }
}