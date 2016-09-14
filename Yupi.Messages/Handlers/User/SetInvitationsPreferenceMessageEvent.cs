using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.User
{
    public class SetInvitationsPreferenceMessageEvent : AbstractHandler
    {
        private readonly IRepository<UserInfo> UserRepository;

        public SetInvitationsPreferenceMessageEvent()
        {
            UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>>();
        }

        public override void HandleMessage(Habbo session, ClientMessage message, IRouter router)
        {
            session.Info.Preferences.IgnoreRoomInvite = message.GetBool();
            UserRepository.Save(session.Info);
        }
    }
}