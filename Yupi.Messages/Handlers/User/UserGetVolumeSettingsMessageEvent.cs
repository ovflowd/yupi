namespace Yupi.Messages.User
{
    using System;

    public class UserGetVolumeSettingsMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message,
            Yupi.Protocol.IRouter router)
        {
            router.GetComposer<LoadVolumeMessageComposer>().Compose(session, session.Info.Preferences);
        }

        #endregion Methods
    }
}