using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class DimmerDataMessageComposer : AbstractComposer<MoodlightData>
    {
        public override void Compose(ISender session, MoodlightData moodlight)
        {
            // Do nothing by default.
        }
    }
}