using Yupi.Protocol;

namespace Yupi.Messages.User
{
    public class CitizenshipStatusMessageComposer : Contracts.CitizenshipStatusMessageComposer
    {
        // TODO Replace value with a proper name
        public override void Compose(ISender session, string value)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendString(value);
                message.AppendInteger(4);
                message.AppendInteger(4); // TODO magic constant
                session.Send(message);
            }
        }
    }
}