using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Guides
{
    public class GuideInviteToRoom : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage message, IRouter router)
        {
            var room = session.Room;

            if ((session.Room == null) || (session.GuideOtherUser == null)) return;

            router.GetComposer<OnGuideSessionInvitedToGuideRoomMessageComposer>()
                .Compose(session.GuideOtherUser, room.Data.Id, room.Data.Name);

            // TODO Is this really required
            router.GetComposer<OnGuideSessionInvitedToGuideRoomMessageComposer>()
                .Compose(session, room.Data.Id, room.Data.Name);
        }
    }
}