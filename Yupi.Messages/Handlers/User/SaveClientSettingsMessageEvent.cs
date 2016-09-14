using System;
using Yupi.Model.Domain.Components;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Model;


namespace Yupi.Messages.User
{
    public class SaveClientSettingsMessageEvent : AbstractHandler
    {
        private IRepository<UserInfo> UserRepository;

        public SaveClientSettingsMessageEvent()
        {
            UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>>();
        }

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
    }
}