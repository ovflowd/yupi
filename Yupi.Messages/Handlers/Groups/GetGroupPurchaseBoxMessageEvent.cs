using System.Linq;
using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Groups
{
    public class GetGroupPurchaseBoxMessageEvent : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            var rooms = session.Info.UsersRooms.Where(x => x.Group == null).ToList();

            router.GetComposer<GroupPurchasePageMessageComposer>().Compose(session, rooms);
        }
    }
}