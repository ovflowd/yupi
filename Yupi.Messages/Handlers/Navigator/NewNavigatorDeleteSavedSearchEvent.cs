using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;
using Yupi.Util;

namespace Yupi.Messages.Navigator
{
    public class NewNavigatorDeleteSavedSearchEvent : AbstractHandler
    {
        private readonly IRepository<UserInfo> UserRepository;

        public NewNavigatorDeleteSavedSearchEvent()
        {
            UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>>();
        }

        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            var searchId = request.GetInteger();

            session.Info.NavigatorLog.RemoveAll(x => x.Id == searchId);

            UserRepository.Save(session.Info);

            router.GetComposer<NavigatorSavedSearchesComposer>().Compose(session, session.Info.NavigatorLog);
        }
    }
}