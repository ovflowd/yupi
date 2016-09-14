using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Rooms
{
    public class GetRoomRightsListMessageEvent : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            if (session.Room != null)
                router.GetComposer<LoadRoomRightsListMessageComposer>().Compose(session, session.Room.Data);
        }
    }
}