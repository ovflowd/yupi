namespace Yupi.Messages.User
{
    using System;

    public class OpenAchievementsBoxMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message,
            Yupi.Protocol.IRouter router)
        {
            session.Router.GetComposer<AchievementListMessageComposer>().Compose(session,
                session.Info.Achievements);
        }

        #endregion Methods
    }
}