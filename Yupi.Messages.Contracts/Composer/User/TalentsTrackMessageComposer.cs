using System.Collections.Generic;
using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class TalentsTrackMessageComposer : AbstractComposer<TalentType, IList<Talent>>
    {
        public override void Compose(ISender session, TalentType trackType, IList<Talent> talents)
        {
            // Do nothing by default.
        }
    }
}