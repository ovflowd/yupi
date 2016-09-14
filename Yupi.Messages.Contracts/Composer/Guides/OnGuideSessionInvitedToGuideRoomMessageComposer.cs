using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class OnGuideSessionInvitedToGuideRoomMessageComposer : AbstractComposer<int, string>
    {
        public override void Compose(ISender session, int roomId, string roomName)
        {
            // Do nothing by default.
        }
    }
}