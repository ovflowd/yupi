using Yupi.Protocol;

namespace Yupi.Messages.Guides
{
    public class HelperToolConfigurationMessageComposer : Contracts.HelperToolConfigurationMessageComposer
    {
        public override void Compose(ISender session, bool onDuty, int guideCount, int helperCount, int guardianCount)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendBool(onDuty);
                message.AppendInteger(guideCount);
                message.AppendInteger(helperCount);
                message.AppendInteger(guardianCount);
                session.Send(message);
            }
        }
    }
}