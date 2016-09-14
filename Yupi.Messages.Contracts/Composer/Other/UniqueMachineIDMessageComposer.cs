using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class UniqueMachineIDMessageComposer : AbstractComposer<string>
    {
        public override void Compose(ISender session, string machineId)
        {
            // Do nothing by default.
        }
    }
}