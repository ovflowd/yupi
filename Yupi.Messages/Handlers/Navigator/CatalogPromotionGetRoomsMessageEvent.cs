using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Navigator
{
    // TODO Isn't this navigator and not catalog?
    public class CatalogPromotionGetRoomsMessageEvent : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            router.GetComposer<CatalogPromotionGetRoomsMessageComposer>().Compose(session, session.Info.UsersRooms);
        }
    }
}