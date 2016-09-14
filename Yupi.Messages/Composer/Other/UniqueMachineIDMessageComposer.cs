using Yupi.Protocol;

namespace Yupi.Messages.Other
{
    public class UniqueMachineIDMessageComposer : Contracts.UniqueMachineIDMessageComposer
    {
        public override void Compose(ISender session, string machineId)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendString(machineId);
                session.Send(message);
            }
        }
    }
}