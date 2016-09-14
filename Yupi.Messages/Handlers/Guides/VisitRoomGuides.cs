using Yupi.Messages.User;
using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Guides
{
    public class VisitRoomGuides : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage message, IRouter router)
        {
            if (session.GuideOtherUser == null)
                return;

            router.GetComposer<RoomForwardMessageComposer>().Compose(session, session.GuideOtherUser.Room.Data.Id);
        }
    }
}