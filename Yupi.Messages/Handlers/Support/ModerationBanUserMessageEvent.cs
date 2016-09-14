namespace Yupi.Messages.Support
{
    using System;

    using Yupi.Controller;
    using Yupi.Model;

    public class ModerationBanUserMessageEvent : AbstractHandler
    {
        #region Fields

        private ModerationTool ModerationTool;

        #endregion Fields

        #region Constructors

        public ModerationBanUserMessageEvent()
        {
            ModerationTool = DependencyFactory.Resolve<ModerationTool>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            if (!session.Info.HasPermission("fuse_ban"))
                return;

            int userId = request.GetInteger();
            string reason = request.GetString();
            int hours = request.GetInteger();

            if (ModerationTool.CanBan(session.Info, userId))
            {
                ModerationTool.BanUser(userId, hours, reason);
            }
        }

        #endregion Methods
    }
}