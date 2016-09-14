using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Groups
{
    public class GetGroupForumsMessageEvent : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            var selectType = request.GetInteger();
            var startIndex = request.GetInteger();

            router.GetComposer<GroupForumListingsMessageComposer>().Compose(session, selectType, startIndex);
        }
    }
}