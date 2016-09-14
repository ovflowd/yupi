namespace Yupi.Messages.User
{
    using System;

    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Domain.Components;
    using Yupi.Model.Repository;

    public class SaveClientSettingsMessageEvent : AbstractHandler
    {
        #region Fields

        private IRepository<UserInfo> UserRepository;

        #endregion Fields

        #region Constructors

        public SaveClientSettingsMessageEvent()
        {
            UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message,
            Yupi.Protocol.IRouter router)
        {
            UserPreferences preferences = session.Info.Preferences;

            // TODO Validate values
            preferences.Volume1 = message.GetInteger();
            preferences.Volume2 = message.GetInteger();
            preferences.Volume3 = message.GetInteger();
            UserRepository.Save(session.Info);
        }

        #endregion Methods
    }
}