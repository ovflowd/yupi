using Yupi.Protocol;

namespace Yupi.Messages.Rooms
{
    public class SpectatorModeMessageComposer : Contracts.SpectatorModeMessageComposer
    {
        public override void Compose(ISender session)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                session.Send(message);
            }
        }
    }
}