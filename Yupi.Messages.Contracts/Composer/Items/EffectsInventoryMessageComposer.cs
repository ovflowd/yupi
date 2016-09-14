using System.Collections.Generic;
using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class EffectsInventoryMessageComposer : AbstractComposer<IList<AvatarEffect>>
    {
        public override void Compose(ISender session, IList<AvatarEffect> effects)
        {
            // Do nothing by default.
        }
    }
}