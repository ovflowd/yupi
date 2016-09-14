using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.User
{
    public class SaveClientSettingsMessageEvent : AbstractHandler
    {
        private readonly IRepository<UserInfo> UserRepository;

        public SaveClientSettingsMessageEvent()
        {
            UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>>();
        }

        public override void HandleMessage(Habbo session, ClientMessage message, IRouter router)
        {
            var preferences = session.Info.Preferences;

            // TODO Validate values
            preferences.Volume1 = message.GetInteger();
            preferences.Volume2 = message.GetInteger();
            preferences.Volume3 = message.GetInteger();
            UserRepository.Save(session.Info);
        }
    }
}