using Yupi.Controller;
using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Support
{
    public class ModerationBanUserMessageEvent : AbstractHandler
    {
        private readonly ModerationTool ModerationTool;

        public ModerationBanUserMessageEvent()
        {
            ModerationTool = DependencyFactory.Resolve<ModerationTool>();
        }

        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            if (!session.Info.HasPermission("fuse_ban"))
                return;

            var userId = request.GetInteger();
            var reason = request.GetString();
            var hours = request.GetInteger();

            if (ModerationTool.CanBan(session.Info, userId)) ModerationTool.BanUser(userId, hours, reason);
        }
    }
}