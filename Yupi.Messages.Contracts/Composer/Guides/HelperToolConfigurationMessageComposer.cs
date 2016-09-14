using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
    public abstract class HelperToolConfigurationMessageComposer : AbstractComposer<bool, int, int, int>
    {
        public override void Compose(Yupi.Protocol.ISender session, bool onDuty, int guideCount, int helperCount,
            int guardianCount)
        {
            // Do nothing by default.
        }
    }
}