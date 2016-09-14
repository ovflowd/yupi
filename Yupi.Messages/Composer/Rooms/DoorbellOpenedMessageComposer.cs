using Yupi.Protocol;

namespace Yupi.Messages.Rooms
{
    public class DoorbellOpenedMessageComposer : Contracts.DoorbellOpenedMessageComposer
    {
        public override void Compose(ISender session)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendString(string.Empty); // TODO What can this be used for?
                session.Send(message);
            }
        }
    }
}